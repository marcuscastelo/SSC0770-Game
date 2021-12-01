using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using TMPro;
using Zenject;

public class ClockDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

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
        timeText.text = Mathf.Ceil(_clock.CurrentTime).ToString("0") + "''";
    }
}
