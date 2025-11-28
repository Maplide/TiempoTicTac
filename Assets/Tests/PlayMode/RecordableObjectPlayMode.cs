using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RecordableObjectPlayModeTests
{
    [UnityTest]
    public IEnumerator RecordableObject_RecordAndReplay_ReproducesPath()
    {
        var go = new GameObject("Recordable_Test");
        var rb = go.AddComponent<Rigidbody2D>();
        var rec = go.AddComponent<RecordableObject>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rec.controlKinematic = true;
        rec.recordInterval = 0.02f;

        go.transform.position = Vector3.zero;

        rec.StartRecording();
        float recordTime = 0;

        while (recordTime < 0.5f)
        {
            go.transform.position += Vector3.right * (5f * Time.deltaTime);
            recordTime += Time.deltaTime;
            yield return null;
        }

        rec.StopRecording();
        Vector3 endRecordedPos = go.transform.position;

        go.transform.position = Vector3.zero;
        rec.StartReplay();

        float timeout = 2f;
        float elapsed = 0f;

        while (rec.IsReplaying && elapsed < timeout)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        Assert.Greater(go.transform.position.x, 0.01f);
        float diff = Mathf.Abs(go.transform.position.x - endRecordedPos.x);
        Assert.LessOrEqual(diff, 0.2f);
    }
}
