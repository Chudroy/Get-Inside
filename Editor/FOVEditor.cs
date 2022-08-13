using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    void OnSceneGUI()
    {
        EnemyFOV enemyFOV = (EnemyFOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyFOV.transform.position, Vector3.forward, Vector3.up, 360, enemyFOV._radius);

        Vector3 viewAngle01 = DirectionFromAngle(enemyFOV.transform.eulerAngles.z, -enemyFOV._angle);
        Vector3 viewAngle02 = DirectionFromAngle(enemyFOV.transform.eulerAngles.z, enemyFOV._angle);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngle01 * enemyFOV._radius);
        Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngle02 * enemyFOV._radius);

        // if (enemyFOV._canSeePlayer)
        // {
        //     Handles.color = Color.green;
        //     Handles.DrawLine(enemyFOV.transform.position, enemyFOV._playerRef.transform.position);
        // }
    }

    Vector3 DirectionFromAngle(float eulerZ, float angleInDegrees)
    {
        angleInDegrees += eulerZ + 90;
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0 );
    }
}
