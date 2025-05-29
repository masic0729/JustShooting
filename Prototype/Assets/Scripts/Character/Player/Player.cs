using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    enum BulletType { Wind, Iced, Fire, Lightning }
    BulletType currentBullet;

    [Header("Components & Stats")]
    public TextMeshProUGUI debugText;
    public PlayerPower powerStats;
    public PlayerBulletData[] bulletDataArray;
    public GameObject[] buffPrefabs, skillPrefabs;


    Dictionary<string, PlayerBulletData> bulletDataDict = new();
    Dictionary<string, GameObject> buffDict = new();
    Dictionary<string, GameObject> skillDict = new();

    float attackTimer;
    const float PowerRestartDelay = 5f;
    public int windBulletHitCount;
    public Transform skillTrans;

    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        if (Time.timeScale <= 0) return;

        base.Update();
        HandleInput();
        UpdateDebugText();
    }

    override protected void Init()
    {
        base.Init();
        InitDic();


        maxMoveX = 9.5f;
        maxMoveY = 4.5f;
        attackDelay = 0.1f;
        SetMoveSpeed(10f);
        powerStats = GetComponent<PlayerPower>();
        invincibilityTime = 2f;
        OnCharacterDamaged += GetDamageEffect;
        OnCharacterDamaged += UpdateHpUI;
        //OnCharacterDeath += HandleDeath;
        OnCharacterDeath += PlayerDeath;

        /*SetHp(100);
        SetMaxHp(GetHp());*/

        hitExplosion = Instantiate(hitExplosion, transform.position, transform.rotation);
        hitExplosion.transform.parent = this.transform;
        hitExplosion.SetActive(false);

        SetCurrentBullet(BulletType.Wind);
        StartCoroutine(powerStats.DefaultPowerUp());
    }

    void InitDic()
    {
        for (int i = 0; i < bulletDataArray.Length; i++)
        {
            var key = bulletDataArray[i].weaponName;
            bulletDataDict[key] = bulletDataArray[i];
            buffDict[key] = buffPrefabs[i];
            skillDict[key] = skillPrefabs[i];
        }
    }

    

    void HandleInput()
    {

        MoveInput();
        HandleAttack();
        HandleWeaponSwitch();

        if(Input.GetKeyDown(KeyCode.F1))
        {
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if(GetIsInvincibility())
            {
                SetIsInvincibility(false);
            }
            else
            {
                SetIsInvincibility(true);

            }
        }
    }

    void MoveInput()
    {
        

        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -maxMoveX) dir += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxMoveX) dir += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < maxMoveY) dir += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -maxMoveY) dir += Vector3.down;

        ObjectMove(dir);
    }

    void HandleAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay * attackStats.attackDelayMultify)
        {
            FireBullet(currentBullet);
            attackTimer = 0;
        }
    }

    void HandleWeaponSwitch()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (powerStats.isPowerMax)
        {
            powerStats.playerPower = 0;
            powerStats.isPowerMax = false;
            powerStats.SetIsActivedSkill(true);
            ActivateSkill();
            Invoke(nameof(ResetPowerRegen), PowerRestartDelay);
        }
        else
        {
            AudioManager.Instance.PlaySFX("WeaponSwitch");
        }

        currentBullet = (BulletType)(((int)currentBullet + 1) % 3);
        SetCurrentBullet(currentBullet);

        
    }

    void SetCurrentBullet(BulletType type)
    {
        currentBullet = type;
        var data = bulletDataDict[type.ToString()];
        attackStats.sprite = data.sprite;
        attackStats.animCtrl = data.animCtrl;
        attackStats.moveSpeed = data.moveSpeed;
        attackStats.attackDelayMultify = data.attackDelayMultify;
        attackStats.damageMultiplier = data.attackMultify;
        powerStats.powerUpValue = data.powerValue;

        windBulletHitCount = 0;

        UI_Manager.instance.UpdateWeaponUI(currentBullet.ToString());
    }

    void FireBullet(BulletType type)
    {
        int count = (type == BulletType.Fire) ? 5 : 1;
        float angle = (type == BulletType.Fire) ? -20f : 0f;

        for (int i = 0; i < count; i++)
        {
            GameObject bullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get();
            ApplyBulletData(ref bullet);
            attackManage.ShootBulletRotate(ref bullet, shootTransform["CommonBullet"], angle);
            angle += 10f;
        }
    }

    void ApplyBulletData(ref GameObject bullet)
    {
        string key = currentBullet.ToString();
        bullet.GetComponent<SpriteRenderer>().sprite = attackStats.sprite;
        bullet.GetComponent<PlayerCommonBullet>().bulletName = key;

        var data = bulletDataDict[key];
        if(key == "Lightning")
        {
            projectileManage.SetProjectileData(ref bullet, attackStats.sprite,
                attackStats.moveSpeed, attackStats.damage * attackStats.damageMultiplier,
                data.lifeTime, "Player");
        }
        else
        {
            projectileManage.SetProjectileData(ref bullet, attackStats.animCtrl,
                attackStats.moveSpeed, attackStats.damage * attackStats.damageMultiplier,
                data.lifeTime, "Player");
        }
    }

    void UpdateHpUI()
    {
        UI_Manager.instance.UpdatePlayerHP(GetHp());
    }

    void GetDamageEffect()
    {
        StartCoroutine(EffectCycle(hitExplosion)); //X4
        //GameObject instance = Instantiate(hitExplosion, skillTrans.position, transform.rotation);
        //instance.gameObject.transform.parent = this.transform;
        

        AudioManager.Instance.PlaySFX("PlayerHitSample");
    }

    void PlayerDeath()
    {
        gameObject.SetActive(false);
        StartCoroutine(EffectCycle(destroyExplosion)); //X4
        Instantiate(destroyExplosion, skillTrans.position, transform.rotation);
        AudioManager.Instance.PlaySFX("PlayerHitSample");
        GameManager.instance.GameEnd(UI_Manager.ScreenInfo.Lose);
    }

    IEnumerator EffectCycle(GameObject effect)
    {
        effect.SetActive(true);
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(ps.main.duration);
        effect.SetActive(false);

    }

    void ActivateSkill()
    {
        Instantiate(skillDict[currentBullet.ToString()]);
    }

    void ResetPowerRegen() => powerStats.SetIsActivedSkill(false);

    public void PowerOn()
    {
        Instantiate(buffDict[currentBullet.ToString()]);
        AudioManager.Instance.PlaySFX("PowerOn");
    }

    public void WindSkill(GameObject bullet, int count)
    {
        StartCoroutine(SkillShoot(bullet, count, 0.8f));
        AudioManager.Instance.PlaySFX("WindSkillShoot");
    }
    public void IcedSkill(GameObject bullet)
    {
        StartCoroutine(SkillShoot(bullet, 15, 0.7f));
        AudioManager.Instance.PlaySFX("IcedSkill");
    }


    IEnumerator SkillShoot(GameObject prefab, int count, float damageRate)
    {
        float delay = attackDelay * attackStats.attackDelayMultify;

        for (int i = 0; i < count; i++)
        {
            GameObject instance = Instantiate(prefab, shootTransform["Skill"].position, shootTransform["Skill"].rotation);
            if (instance != null)
            {
                instance.tag = "Player";
                instance.GetComponent<Projectile>().SetMoveSpeed(attackStats.moveSpeed * 2f);
                instance.GetComponent<Projectile>().SetDamage(attackStats.damage * attackStats.damageMultiplier * damageRate);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    /*void HandleDeath()
    {
        GameManager.instance.isGameEnd = true;
        UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Lose);
    }*/

    void UpdateDebugText()
    {
        if (debugText == null) return;

        debugText.text = $"hp: {GetHp()}\n" +
                    $"weapon: {currentBullet}\n" +
                    $"damage: {attackStats.damage * attackStats.damageMultiplier}\n" +
                    $"AttackDelay: {attackDelay * attackStats.attackDelayMultify}\n" +
                    $"moveSpeed: {attackStats.moveSpeed}\n" +
                    $"power: {powerStats.powerUpValue}\n" +
                    $"playerMoveSpeed: {moveSpeed * objectMoveSpeedMultify}\n" +
                    $"PlayerPowerValue: {powerStats.playerPower}\n" + 
                    $"무적 여부: { GetIsInvincibility().ToString()}";
    }

    

    public string GetPlayerWeaponName() => currentBullet.ToString();
}
