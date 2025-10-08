using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommonBullet : Bullet
{
    //��� �Ӽ��� �� Ȯ���Ϸ��� ���ڰ�
    public string bulletName;
    float windAttackDelayTransValue = 0.05f;
    bool isCritical = false;                                //ġ��Ÿ �߻��ϴ� �߻�ü�� �� Ȯ��
    Player player;
    [SerializeField] GameObject[] pHitEffects;
    protected override void OnEnable()
    {
        base.OnEnable();
        Init();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void LateUpdate()
    {
        if (bulletName == "Fire")
        {
            SetLifeTime(0.25f);
            //Debug.Log("�� �ҼӼ��̶� ª��");
        }
    }

    protected override void Init()
    {
        base.Init();
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        switch (bulletName)
        {
            case "Wind":
                ProjectileHitEffect = pHitEffects[0];
                hitSoundName = "WindHit";
                break;
            case "Iced":
                ProjectileHitEffect = pHitEffects[1];
                hitSoundName = "IcedHit";

                break;
            case "Fire":
                ProjectileHitEffect = pHitEffects[2];
                hitSoundName = "FireHit";
                break;
        }

    }

    public void SetCritical()
    {
        damage *= StatManager.instance.criticalMultify;
        Debug.Log("ũ��Ƽ�� ó����" + damage.ToString() + " " + this.gameObject.name);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        

        base.OnTriggerEnter2D(collision);
        if (bulletName == player.GetPlayerWeaponName() && player.windBulletHitCount < 6 &&
            collision.transform.tag =="Enemy" && bulletName == "Wind" && player != null)
        {
            player.attackStats.attackDelayMultify -= windAttackDelayTransValue;
            player.windBulletHitCount++;
        }
        

    }
}
