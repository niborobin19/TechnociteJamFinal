using System.Collections.Generic;
using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class SuspectBoardItem : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private ScriptableSuspect _suspect;

        [SerializeField]
        private SuspectVariable _displayedSuspect;

        [SerializeField]
        private AudioSourceVariable _channel;

        [Header("Variables")]
        [SerializeField]
        private AudioClip _interactionSound;

        [SerializeField]
        private float _interactionVolumeCoefficient = 1.0f;

        [Header("References")]
        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private Transform _pin;

        #endregion


        #region Unity API

        private void Awake() 
        {
            _linesToClues = new Dictionary<ScriptableClue, GameObject>();
            UpdateDisplay();  
        }

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            _displayedSuspect.Suspect = _suspect;
            _channel.Source.PlayOneShot(_interactionSound, _interactionVolumeCoefficient);
        }

        public void UpdateDisplay()
        {
            _sprite.sprite = _suspect.Sprite;
            gameObject.SetActive(_suspect.IsDisplayable);
            UpdateLines();
        }

        private void UpdateLines()
        {
            foreach (var item in _linesToClues)
            {
                item.Value.SetActive(_suspect.HasLinkTo(item.Key));
            }
        }

        public Transform GetPin()
        {
            return _pin;
        }

        public void AddLineEntry(ScriptableClue clue, GameObject line)
        {
            if(_linesToClues.ContainsKey(clue)) return;
            
            _linesToClues.Add(clue, line);
        }

        #endregion


        #region Private
        private Dictionary<ScriptableClue, GameObject> _linesToClues;

        #endregion
    }
}