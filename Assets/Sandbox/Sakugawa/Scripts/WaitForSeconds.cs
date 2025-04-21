using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class WaitForSeconds : IEnumerator
    {
        private readonly float _seconds;
        private float _elapsedTime;
        
        private float _nextStepTime;
        public void SetNextStepTime(float time)
        {
            _nextStepTime = time;
        }
        
        public WaitForSeconds(float seconds)
        {
            _seconds = seconds;
            _elapsedTime = 0f;
        }
        
        public object Current => null;
        
        public bool MoveNext()
        {
            _elapsedTime += _nextStepTime;
            if (_elapsedTime >= _seconds)
            {
                return false;
            }
            _nextStepTime = 0f;
            return true;
        }
        
        public void Reset()
        {
            _elapsedTime = 0f;
        }
    }
}