using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Quests;

namespace Actors
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] UIEventsSO uiEventsSO;
        QuestLine questLine;
        // Start is called before the first frame update
        void Start()
        {
            questLine = GetComponent<QuestLine>();
        }

        public void OnDialogueInteracition()
        {
            uiEventsSO.OnDialogueInteractionEvents(questLine.id);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerActor _player = other.GetComponent<PlayerActor>();
            if(_player != null)
            {
                _player.interactionEvents += OnDialogueInteracition;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerActor _player = other.GetComponent<PlayerActor>();
            if (_player != null)
            {
                _player.interactionEvents -= OnDialogueInteracition;
            }
        }
    }
}

