using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class RealmExample : MonoBehaviour
{
    // Resources:
    // https://github.com/realm/realm-dotnet

    [SerializeField] private int hitCount = 0;

    private Realm realm;
    private HitCountEntity hitCountEntity;

    private void Awake()
    {
        // Open a database connection.
        realm = Realm.GetInstance();

        // Read the hit count data from the database.
        hitCountEntity = realm.Find<HitCountEntity>(1);
        if (hitCountEntity != null)
        {
            hitCount = hitCountEntity.Value;
        }
    }

    private void OnDestroy()
    {

        // Update the hit count in the database.
        realm.Write(() =>
        {
            // Create the object if it does not exist.
            if (hitCountEntity == null)
            {
                hitCountEntity = new HitCountEntity(1);
                realm.Add(hitCountEntity);
            }
            hitCountEntity.Value = hitCount;
        });
        realm.Dispose();
    }

    private void OnMouseDown()
    {
        hitCount++;
    }
}
