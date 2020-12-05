using System;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public Vector3 _startPosition { get; private set; }
    public Vector3? _destination;
    public float _elapsedLerpTime { get; private set; }
    [SerializeField] float _totalLerpDuration = 0.3f;
    public Action _onCompleteCallback { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_destination.HasValue == false) return;
        _elapsedLerpTime += Time.deltaTime;

        float percent = _elapsedLerpTime / _totalLerpDuration;

        gameObject.transform.position = Vector3.Lerp(_startPosition, _destination.Value,  percent);

        if (_elapsedLerpTime > _totalLerpDuration)
        {
            //call the callback and reset values to 0
            _onCompleteCallback?.Invoke();
            _elapsedLerpTime = 0f;
            _destination = null;
            //unsubscribe if needed
            if (_onCompleteCallback != null)
            {
                //we have to NullCheck because we dont want to UNSUBSCRIBE if we don't NEED to
                Delegate[] callbackList = _onCompleteCallback.GetInvocationList();
                foreach(Action callback in callbackList)
                {
                    _onCompleteCallback -= callback;
                }
            }
        }

    }

    //Whoever calls this method, can call as many callbacks as they want. They will all get executed
    public void MoveTo(Vector3 destination, params Action[] onComplete)
    {
        //total lerp duration defined here by distance / speed.       
        _startPosition = gameObject.transform.position;
        _destination = destination;
        var distance = Vector3.Distance(_startPosition, _destination.Value);
        // _totalLerpDuration = distance / _animSpeed;
        // _totalLerpDuration = .5f;
        foreach(Action callback in onComplete)
        {
            _onCompleteCallback += callback;
        }
    }
}
