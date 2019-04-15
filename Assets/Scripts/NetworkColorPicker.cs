using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkColorPicker : NetworkBehaviour {

    [SyncVar] private Color randomColor;

    void Start()
    {

        if (isLocalPlayer)
        {
            randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            GetComponent<Renderer>().material.color = randomColor;
        }

    }

    [Command]
    void Cmd_ProvideColorToServer(Color c)
    {

        randomColor = c;
    }

    [ClientCallback]
    void TransmitColor()
    {
        if (isLocalPlayer)
        {
            Cmd_ProvideColorToServer(randomColor);
        }
    }

    public override void OnStartClient()
    {
        StartCoroutine(UpdateColor(1.5f));

    }

    IEnumerator UpdateColor(float time)
    {

        float timer = time;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            TransmitColor();
            if (!isLocalPlayer)
                GetComponent<Renderer>().material.color = randomColor;


            yield return null;
        }


    }
}