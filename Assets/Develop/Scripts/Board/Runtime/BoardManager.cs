using UnityEngine;
using Interactable.Runtime;
using Scriptables.Runtime;

namespace Board.Runtime
{
    public class BoardManager : MonoBehaviour
    {
        #region Exposed
        [Header("Variables")]
        [SerializeField]
        private Material _lineMaterial;

        [SerializeField]
        private Color _lineStartColor;

        [SerializeField]
        private Color _lineEndColor;

        [SerializeField]
        private float _lineWidth = 0.1f;


        [Header("References")]
        [SerializeField]
        private Transform _cluesRoot;

        [SerializeField]
        private Transform _suspectRoot;

        #endregion


        #region Unity API

        private void Start() 
        {
            TurnAllOn();
            GenerateLines();
            UpdateDisplay();
        }

        #endregion


        #region Main

        private void TurnAllOn()
        {
            for (int i = 0; i < _suspectRoot.childCount; i++)
            {
                _suspectRoot.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < _cluesRoot.childCount; i++)
            {
                _cluesRoot.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void GenerateLines()
        {
            foreach (var suspect in _suspectRoot.GetComponentsInChildren<SuspectBoardItem>())
            {
                var suspectPin = suspect.GetPin();

                foreach (var clue in _cluesRoot.GetComponentsInChildren<ClueBoardItem>())
                {
                    var cluePin = clue.GetPin();

                    var line = GenerateLineBetween(suspectPin, cluePin);
                    line.SetActive(false);

                    suspect.AddLineEntry(clue.GetClue(), line);
                }
            }
        }

        private void UpdateDisplay()
        {
            foreach (var suspect in _suspectRoot.GetComponentsInChildren<SuspectBoardItem>())
            {
                suspect.UpdateDisplay();
            }
            foreach (var clue in _cluesRoot.GetComponentsInChildren<ClueBoardItem>())
            {
                clue.UpdateDisplay();
            }
        }

        #endregion


        #region Utils

        private GameObject GenerateLineBetween(Transform start, Transform end)
        {
            var line = new GameObject();
            line.transform.parent = start;
            line.transform.localPosition = Vector3.zero;

            var lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = _lineMaterial;

            lineRenderer.startColor = _lineStartColor;
            lineRenderer.endColor = _lineEndColor;
            
            lineRenderer.startWidth = _lineWidth;
            lineRenderer.endWidth = _lineWidth;
            
            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, end.position);

            return line;
        }

        #endregion


        #region Private

        #endregion
    }
}