using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSubjectSelect : MonoBehaviour
{
    [SerializeField] bool logicGateQuestionsActive;
    [SerializeField]  bool binaryQuestionsActive;
    [SerializeField]  bool booleanAlgebraQuestionsActive;
    [SerializeField] bool soundQuestionsActive;
    [SerializeField] bool dataRepresentationQuestionsActive;

    static QuestionSubjectSelect instance;
    public static QuestionSubjectSelect Instance { get { return instance; } }

    public bool LogicGateQuestionsActive { get { return logicGateQuestionsActive; } }
    public bool BinaryQuestionsActive { get { return binaryQuestionsActive; } }
    public bool BooleanAlgebraQuestionsActive { get { return booleanAlgebraQuestionsActive; } }
    public bool SoundQuestionsActive { get { return soundQuestionsActive; } }
    public bool DataRepresentationQuestionsActive { get { return dataRepresentationQuestionsActive; } }
    public bool CanProgress { get { return logicGateQuestionsActive || binaryQuestionsActive || booleanAlgebraQuestionsActive || soundQuestionsActive || dataRepresentationQuestionsActive; } }

    void Awake()
    {
        // This object will always be present, even when switching levels
        Object.DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // Alternate the status of the topics checkboxes
    public void ChangeLogicGateStatus()
    {
        logicGateQuestionsActive = !logicGateQuestionsActive;
    }
    public void ChangeBinaryStatus()
    {
        binaryQuestionsActive = !binaryQuestionsActive;
    }
    public void ChangeBooleanAlgebraStatus()
    {
        booleanAlgebraQuestionsActive = !booleanAlgebraQuestionsActive;
    }
    public void ChangeSoundStatus()
    {
        soundQuestionsActive = !soundQuestionsActive;
    }
    public void ChangeDataRepresentationStatus()
    {
        dataRepresentationQuestionsActive = !dataRepresentationQuestionsActive;
    }
}
