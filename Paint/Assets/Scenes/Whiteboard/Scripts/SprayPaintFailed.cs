using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SprayPaintFailed : MonoBehaviour
{
    public ParticleSystem parSys;
    public List<ParticleCollisionEvent> collisionEvents;
    public int penSize;

    private RaycastHit hit;
    private Vector2 hitPos;
    public Color[] colours;

    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        colours = Enumerable.Repeat(parSys.main.startColor.color, penSize * penSize).ToArray();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Whiteboard"))
        {
            Whiteboard whiteboard = other.GetComponent<Whiteboard>();
            int numCollisionEvents = parSys.GetCollisionEvents(other, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                paint(whiteboard, i);
            }
        }
    }

    private void paint(Whiteboard whiteboard, int i)
    {
        if (Physics.Raycast(collisionEvents[i].intersection, transform.forward, out hit, collisionEvents[i].intersection.y))
        {
            hitPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

            var x = (int)(hitPos.x * whiteboard.textureSize.x - (penSize / 2));
            var y = (int)(hitPos.y * whiteboard.textureSize.y - (penSize / 2));

            whiteboard.texture.SetPixels(x, y, penSize, penSize, colours);
            whiteboard.texture.Apply();
        }
    }
}
