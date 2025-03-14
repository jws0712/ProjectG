namespace Agent
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEngine;
    using UnityEditor;

    public abstract class Character : MonoBehaviour
    {
        protected abstract void TakeDamge(float damage);

        protected abstract void Die();
    }
}

