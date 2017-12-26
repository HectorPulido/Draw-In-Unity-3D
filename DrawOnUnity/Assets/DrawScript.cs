using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
Hola amigo, este paquete ha sido creado por Hector Pulido de youtube
YOUTUBE:https://www.youtube.com/channel/UCS_iMeH0P0nsIDPvBaJckOw
Apoyame en PATREON: https://www.patreon.com/HectorPulido
Sígueme en TWITTER: https://twitter.com/Hector_Pulido_
Acompañanos en FACEBOOK: https://www.facebook.com/groups/Unity3DenEspanol
Unetenos en GOOGLE+: https://plus.google.com/communities/103453576782892092582
*/

public class DrawScript : MonoBehaviour
{

    public bool usePhysics = true;
    public float colliderThickness = 0.1f;
    public float vertexPrecision = 0.01f;
    public Shader shader;
    public Color color;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject()) // Avoid Draw On Ui Element            
                StartCoroutine(draw());
        }
    }

    Vector3 newVertex;
    Vector3 lastVertex;
    IEnumerator draw()
    {
        //Setting up the line render
        LineRenderer r = new GameObject().AddComponent<LineRenderer>();
        r.transform.SetParent(transform);
        r.startWidth = 0.1f;
        r.endWidth = 0.1f;
        r.material = new Material(shader);
        r.material.color = color;

        List<Vector3> posiciones = new List<Vector3>();

        while (Input.GetMouseButton(0)) // Adding Mouse Points To Line Render
        {
            newVertex = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 5;
            if (Vector3.Distance(lastVertex, newVertex) >= vertexPrecision) //Checking distance between vertx
            {
                posiciones.Add(newVertex);
                r.positionCount = posiciones.Count;
                r.SetPositions(posiciones.ToArray());
                lastVertex = newVertex;
            }
            yield return new WaitForEndOfFrame();
        }
        r.useWorldSpace = false;

        if (usePhysics) // Add Physics to line render
        {
            List<Vector2> posiciones2 = new List<Vector2>();

            for (int i = 0; i < posiciones.Count; i++) // Forward position to add
            {
                posiciones2.Add(new Vector2(posiciones[i].x, posiciones[i].y));
            }

            for (int i = posiciones.Count - 1; i > 0; i--) //Backward position to add
            {
                posiciones2.Add(new Vector2(posiciones[i].x, posiciones[i].y + colliderThickness));
            }

            PolygonCollider2D col = r.gameObject.AddComponent<PolygonCollider2D>();
            col.points = posiciones2.ToArray();

            col.gameObject.AddComponent<Rigidbody2D>();
        }
    }
}