﻿using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Yarn.Unity;

public class DialogueRunnerTests
{
    [UnityTest]
    public IEnumerator HandleLine_OnValidYarnFile_SendCorrectLinesToUI()
    {
        SceneManager.LoadScene("DialogueRunnerTest");
        bool loaded = false;
        SceneManager.sceneLoaded += (index, mode) => {
            loaded = true;
        };
        yield return new WaitUntil(() => loaded);

        var runner = GameObject.FindObjectOfType<DialogueRunner>();
        DialogueRunnerMockUI dialogueUI = GameObject.FindObjectOfType<DialogueRunnerMockUI>();

        runner.StartDialogue();
        yield return null;

        Assert.That(string.Equals(dialogueUI.CurrentLine, "Spieler: Kannst du mich hören?"));
        dialogueUI.MarkLineComplete();
        
        Assert.That(string.Equals(dialogueUI.CurrentLine, "NPC: Klar und deutlich."));
        dialogueUI.MarkLineComplete();

        Assert.AreEqual(2, dialogueUI.CurrentOptions.Count);
        Assert.AreEqual("Mir reicht es.", dialogueUI.CurrentOptions[0]);
        Assert.AreEqual("Nochmal!", dialogueUI.CurrentOptions[1]);
    }
}