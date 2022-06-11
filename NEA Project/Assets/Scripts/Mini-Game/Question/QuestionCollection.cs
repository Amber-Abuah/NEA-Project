using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionCollection
{
    QuestionSubjectSelect questionSubjectSelect;

    [SerializeField] List<List<Question>> allQuestions;
    [SerializeField] List<Question> logicGateQuestions;
    [SerializeField] List<Question> binaryQuestions;
    [SerializeField] List<Question> booleanAlgebraQuestions;
    [SerializeField] List<Question> soundQuestions;
    [SerializeField] List<Question> dataRepresentationQuestions;

    [SerializeField] List<string> topicNames;
    [SerializeField] List<int> topicIndexes;

    public List<List<Question>> AllQuestions { get { return allQuestions; } }
    public List<Question> LogicGateQuestions { get { return logicGateQuestions; } }
    public List<Question> BinaryQuestions { get { return logicGateQuestions; } }
    public List<Question> BooleanAlgebraQuestions { get { return booleanAlgebraQuestions; } }
    public List<Question> SoundQuestions { get { return soundQuestions; } }
    public List<Question> DataRepresentationQuestions { get { return dataRepresentationQuestions; } }

    public List<string> TopicNames { get { return topicNames; } }
    public List<int> TopicIndexes { get { return topicIndexes; } }

    void AddTopicQuestions(List<Question> questions, string topicName, int topicIndex)
    {
        allQuestions.Add(questions);
        topicNames.Add(topicName);
        topicIndexes.Add(topicIndex);
    }

    public QuestionCollection()
    {
        questionSubjectSelect = QuestionSubjectSelect.Instance;

        // Initialize all lists
        allQuestions = new List<List<Question>>();

        logicGateQuestions = new List<Question>();
        binaryQuestions = new List<Question>();
        booleanAlgebraQuestions = new List<Question>();
        soundQuestions = new List<Question>();
        dataRepresentationQuestions = new List<Question>();

        topicNames = new List<string>();
        topicIndexes = new List<int>();

        if (questionSubjectSelect == null)
        {
            AddTopicQuestions(logicGateQuestions, "Logic Gates", 1);
            AddTopicQuestions(binaryQuestions, "Binary", 2);
        }
        else
        {

            // Add topic questions, depending on whether they were selected in the preplay level
            if (questionSubjectSelect.LogicGateQuestionsActive)
                AddTopicQuestions(logicGateQuestions, "Logic Gates", 1);

            if (questionSubjectSelect.BinaryQuestionsActive)
                AddTopicQuestions(binaryQuestions, "Binary", 2);

            if (questionSubjectSelect.BooleanAlgebraQuestionsActive)
                AddTopicQuestions(booleanAlgebraQuestions, "Boolean Algebra", 3);

            if (questionSubjectSelect.SoundQuestionsActive)
                AddTopicQuestions(soundQuestions, "Sound", 4);

            if (questionSubjectSelect.DataRepresentationQuestionsActive)
                AddTopicQuestions(dataRepresentationQuestions, "Data Representation", 5);
        }
    }
}