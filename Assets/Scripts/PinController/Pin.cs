using UnityEngine;
using TMPro;

public class Pin : MonoBehaviour
{
    private bool _done;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.CompareTag("Ball") || collision.collider.CompareTag("Pin")) && !_done)
        {
          
            float velocity = GetComponent<Rigidbody>().velocity.magnitude;
            if (velocity < 10)
            {
               
                var ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
                
                 
                ball.Point += 1;
                ball.totalPoints += 1;

                 
                var pointText = GameObject.FindGameObjectWithTag("point").GetComponent<TextMeshProUGUI>();
                if (pointText != null)
                {
                    pointText.text = $" Fallen Pins: {ball.Point}";
                }

                _done = true;
            }
        }
    }
}
