using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SprayPaint : MonoBehaviour
{
    [SerializeField] private Transform cap;
    [SerializeField] private int farHitSize;
    [SerializeField] private int medHitSize;
    [SerializeField] private int closeHitSize;

    private Renderer renderer;
    private Color[] colours;

    public ParticleSystem parSys;
    RaycastHit farHit;
    float farHitDis;
    RaycastHit medHit;
    float medHitDis;
    RaycastHit closeHit;
    float closeHitDis;

    private Whiteboard whiteboard;
    private Vector2 touchPos, lastTouchPos;
    private bool touchedLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        closeHitDis = 0.87f;
        medHitDis = 1.73f;
        farHitDis = 2.6f;

        renderer = cap.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parSys.isEmitting)
        {
            if (Physics.Raycast(cap.position, transform.forward, out closeHit, closeHitDis))
            {
                if (closeHit.transform.CompareTag("Whiteboard"))
                {
                    if (whiteboard == null)
                    {
                        whiteboard = closeHit.transform.GetComponent<Whiteboard>();
                    }

                    colours = Enumerable.Repeat(renderer.material.color, closeHitSize * closeHitSize).ToArray();

                    touchPos = new Vector2(closeHit.textureCoord.x, closeHit.textureCoord.y);

                    var x = (int)(touchPos.x * whiteboard.textureSize.x - (closeHitSize / 2));
                    var y = (int)(touchPos.y * whiteboard.textureSize.y - (closeHitSize / 2));

                    if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x)
                    {
                        return;
                    }

                    if (touchedLastFrame)
                    {
                        whiteboard.texture.SetPixels(x, y, closeHitSize, closeHitSize, colours);

                        for (float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            var lerpx = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                            var lerpy = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                            whiteboard.texture.SetPixels(lerpx, lerpy, closeHitSize, closeHitSize, colours);
                        }

                        whiteboard.texture.Apply();
                    }

                    lastTouchPos = new Vector2(x, y);
                    touchedLastFrame = true;
                    return;
                }
            }

            if (Physics.Raycast(cap.position, transform.forward, out medHit, medHitDis))
            {
                if (medHit.transform.CompareTag("Whiteboard"))
                {
                    if (whiteboard == null)
                    {
                        whiteboard = medHit.transform.GetComponent<Whiteboard>();
                    }

                    colours = Enumerable.Repeat(renderer.material.color, medHitSize * medHitSize).ToArray();

                    touchPos = new Vector2(medHit.textureCoord.x, medHit.textureCoord.y);

                    var x = (int)(touchPos.x * whiteboard.textureSize.x - (medHitSize / 2));
                    var y = (int)(touchPos.y * whiteboard.textureSize.y - (medHitSize / 2));

                    if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x)
                    {
                        return;
                    }

                    if (touchedLastFrame)
                    {
                        whiteboard.texture.SetPixels(x, y, medHitSize, medHitSize, colours);

                        for (float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            var lerpx = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                            var lerpy = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                            whiteboard.texture.SetPixels(lerpx, lerpy, medHitSize, medHitSize, colours);
                        }

                        whiteboard.texture.Apply();
                    }

                    lastTouchPos = new Vector2(x, y);
                    touchedLastFrame = true;
                    return;
                }
            }
            if (Physics.Raycast(cap.position, transform.forward, out farHit, farHitDis))
            {
                if (farHit.transform.CompareTag("Whiteboard"))
                {
                    if (whiteboard == null)
                    {
                        whiteboard = farHit.transform.GetComponent<Whiteboard>();
                    }

                    colours = Enumerable.Repeat(renderer.material.color, farHitSize * farHitSize).ToArray();

                    touchPos = new Vector2(farHit.textureCoord.x, farHit.textureCoord.y);

                    var x = (int)(touchPos.x * whiteboard.textureSize.x - (farHitSize / 2));
                    var y = (int)(touchPos.y * whiteboard.textureSize.y - (farHitSize / 2));

                    if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x)
                    {
                        return;
                    }

                    if (touchedLastFrame)
                    {
                        whiteboard.texture.SetPixels(x, y, farHitSize, farHitSize, colours);

                        for (float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            var lerpx = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                            var lerpy = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                            whiteboard.texture.SetPixels(lerpx, lerpy, farHitSize, farHitSize, colours);
                        }

                        whiteboard.texture.Apply();
                    }

                    lastTouchPos = new Vector2(x, y);
                    touchedLastFrame = true;
                    return;
                }
            }
        }
        whiteboard = null;
        touchedLastFrame = false;
    }
}
