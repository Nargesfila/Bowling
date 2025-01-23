using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public float startSpeed = 40f;
    private bool _ballMoving;
    private Transform _arrow;
    private Transform _startPosition;
    private List<GameObject> _pins = new();
    private readonly Dictionary<GameObject, Transform> _pinsDefaultTransform = new();

    public int Point { get; set; }
    public int totalPoints; 
    private TextMeshProUGUI feedBack;

    [SerializeField] private Animator cameraAnim;

    void Start()
    {
        _arrow = GameObject.FindGameObjectWithTag("Arrow").transform;
        rb = GetComponent<Rigidbody>();
        _startPosition = transform;
        _pins = GameObject.FindGameObjectsWithTag("Pin").ToList();

        foreach (var pin in _pins)
        {
            _pinsDefaultTransform.Add(pin, pin.transform);
        }

        feedBack = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (_ballMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        cameraAnim.SetTrigger("Go");
        cameraAnim.SetFloat("CameraSpeed", _arrow.transform.localScale.z);
        _ballMoving = true;
        _arrow.gameObject.SetActive(false);
        rb.isKinematic = false;

        Vector3 forceVector = _arrow.forward * (startSpeed * _arrow.transform.localScale.z);
        Vector3 forcePosition = transform.position + (transform.right * 0.5f);
        rb.AddForceAtPosition(forceVector, forcePosition, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(7);

        _ballMoving = false;
        GenerateFeedBack();
        ResetBall();
    }

    private void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = _startPosition.position;
        _arrow.gameObject.SetActive(true);
    }

    private void GenerateFeedBack()
    {
        feedBack.text = Point switch
        {
            0 => "ohhh come on :)",
            > 0 and < 3 => "Not Bad...",
            >= 3 and < 10 => "Nice!",
            _ => "Wow Perfect!"
        };
        feedBack.GetComponent<Animator>().SetTrigger("Show");
    }

    public bool CheckAllPinsFallen()
    {
        foreach (var pin in _pins)
        {
            if (pin.activeSelf) return false; 
        }
        return true;
    }
}
