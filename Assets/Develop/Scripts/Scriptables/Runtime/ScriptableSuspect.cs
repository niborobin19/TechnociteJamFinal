using System.Collections.Generic;
using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Suspect/Instance", fileName = "New Suspect")]
   public class ScriptableSuspect : ScriptableObject
    {
        #region Exposed
        [Header("Variables")]
        [SerializeField]
        private string _name;

        [SerializeField, TextArea]
        private string _description;

        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private int _displayClueCount;

        [SerializeField]
        private bool _isCulprit;

        [SerializeField]
        private string _deductionTitle;

        [SerializeField, TextArea]
        private string _deductionStory;

        [SerializeField]
        private AudioClip _deductionSpeech;

        [SerializeField]
        private float _deductionSpeechVolume = 1.0f;

        [SerializeField]
        private string _endingTitle;

        [SerializeField, TextArea]
        private string _endingStory;

        [SerializeField]
        private AudioClip _endingSpeech;

        [SerializeField]
        private float _endingSpeechVolume = 1.0f;

        [Header("Datas")]
        [SerializeField]
        private List<ScriptableClue> _relatedClues;

        [Header("Event")]
        [SerializeField]
        private ScriptableEvent _onSuspectChanged;

        #endregion


        #region Properties

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                _description = value;
            }
        }

        public Sprite Sprite
        {
            get => _sprite;

            set
            {
                _sprite = value;
            }
        }

        public int DisplayClueCount
        {
            get => _displayClueCount;

            set
            {
                _displayClueCount = value;
            }
        }

        public bool IsCulprit
        {
            get => _isCulprit;

            set
            {
                _isCulprit = value;
            }
        }

        public string DeductionTitle
        {
            get => _deductionTitle;

            set
            {
                _deductionTitle = value;
            }
        }

        public string DeductionStory
        {
            get => _deductionStory;

            set
            {
                _deductionStory = value;
            }
        }

        public AudioClip DeductionSpeech
        {
            get => _deductionSpeech;

            set
            {
                _deductionSpeech = value;
            }
        }

        public float DeductionSpeechVolume
        {
            get => _deductionSpeechVolume;

            set
            {
                _deductionSpeechVolume = value;
            }
        }

        public string EndingStory
        {
            get => _endingStory;

            set
            {
                _endingStory = value;
            }
        }

        public string EndingTitle
        {
            get => _endingTitle;

            set
            {
                _endingTitle = value;
            }
        }

        public AudioClip EndingSpeech
        {
            get => _endingSpeech;

            set
            {
                _endingSpeech = value;
            }
        }

        public float EndingSpeechVolume
        {
            get => _endingSpeechVolume;

            set
            {
                _endingSpeechVolume = value;
            }
        }

        public List<ScriptableClue> RelatedClues
        {
            get => _relatedClues;

            set
            {
                _relatedClues = value;
            }
        }

        public int ClueScore
        {
            get => ComputeScore();
        }

        public bool IsDisplayable
        {
            get => ComputeIsDisplayable();
        }

        public bool IsChargeable
        {
            get => ComputeIsChargeable();
        }

        #endregion


        #region Unity API

        private void OnEnable() 
        {
            _linkedClues = new List<ScriptableClue>();    
        }

        #endregion


        #region Main

        public void LinkClue(ScriptableClue clue)
        {
            if(_linkedClues.Contains(clue)) return;

            _linkedClues.Add(clue);
            _onSuspectChanged.Raise();
        }

        public void UnlinkClue(ScriptableClue clue)
        {
            if(!_linkedClues.Contains(clue)) return;

            _linkedClues.Remove(clue);
            _onSuspectChanged.Raise();
        }

        public void UnlinkAll()
        {
            _linkedClues = new List<ScriptableClue>();
            _onSuspectChanged.Raise();
        }

        public bool HasLinkTo(ScriptableClue clue)
        {
            return _linkedClues.Contains(clue);
        }

        #endregion


        #region Utils

        private int ComputeScore()
        {
            _clueScore = 0;

            foreach(var clue in _linkedClues)
            {
                var valid = _relatedClues.Contains(clue);

                _clueScore += valid ? 1 : -1;
            } 

            return _clueScore;
        }

        private bool ComputeIsChargeable()
        {
            return ClueScore == _relatedClues.Count;
        }

        private bool ComputeIsDisplayable()
        {
            var discoveredCount = 0;

            foreach (var clue in RelatedClues)
            {
                discoveredCount += clue.IsDiscovered ? 1 : 0;
            }

            return discoveredCount >= DisplayClueCount;
        }

        #endregion


        #region Private

        private List<ScriptableClue> _linkedClues;
        private int _clueScore;

        #endregion
    }
}