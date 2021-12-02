using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using TMPro;
using Zenject;

public class ClockDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] [Range(0.1f, 10f)] private float flashingInterval;

    private Clock _clock;
    public Clock Clock => _clock;

    [Inject]
    public void Construct(Clock clock)
    {
        _clock = clock;
    }

    void Awake()
    {
        Assert.IsNotNull(timeText);
    }
    
    void Start() => ShowTime();
    void Update() => ShowTime();

    private void ShowTime()
    {
        bool isLate = _clock.CurrentTime == 0;
        bool isFlashFrame = Mathf.Ceil(Time.realtimeSinceStartup)%(flashingInterval*2) <= flashingInterval;
        timeText.text = Mathf.Ceil(_clock.CurrentTime).ToString("0") + "''";
        timeText.color = (isFlashFrame && isLate) ? Color.red : Color.white;
    }
}
