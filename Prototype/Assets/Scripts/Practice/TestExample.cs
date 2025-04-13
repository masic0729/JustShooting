using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExample : MonoBehaviour
{
    public enum BodyType { Static, Kinematic, Dynamic }
    public BodyType bodyType;

    public float mass;
    public float drag;
    public bool useGravity;
}
