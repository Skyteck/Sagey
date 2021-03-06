﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Managers
{
    public class DialogManager
    {
        public event EventHandler DialogPlayed;
        public event EventHandler BankOpened;
        public event EventHandler QuestStarted;

        List<Dialog> _DialogList;
        public Dialog CurrentDialog;

        QuestManager _QuestManager;

        public DialogManager(QuestManager qm)
        {
            _QuestManager = qm;
            _DialogList = new List<Dialog>();
        }

        public void LoadDialog(string path)
        {
            //TODO: dispose object
            var file = System.IO.File.ReadAllText(path);
            _DialogList = JsonConvert.DeserializeObject<List<Dialog>>(file);
        }

        public void PlayMessage(string msgID)
        {
            Dialog newDialog = new Dialog();
            newDialog = _DialogList.Find(x => x.ID == msgID);
            if(newDialog == null || msgID == String.Empty)
            {
                Console.WriteLine("Line: " + msgID + " MISSING!");
                Console.WriteLine("Previous dialog: " + CurrentDialog.ID);
            }
            CurrentDialog = newDialog;
            OnDialogPlayed();
        }

        public void PlayMessage(DialogOption option)
        {
            string msgID = option.NextMsgID;
            PlayMessage(msgID);

            if(option.Command != null)
            {
                if(option.Command == "Open Bank")
                {
                    OnBankOpened();
                }
                else if(option.Command.Contains("Start Quest"))
                {
                    string QuestID = option.Command;
                    QuestID = QuestID.Replace("Start Quest:", "").Trim();
                    _QuestManager.ActivateQuest(QuestID);
                }
            }

        }

        public void OnDialogPlayed()
        {
            DialogPlayed?.Invoke(this, EventArgs.Empty);
        }

        public void OnBankOpened()
        {
            BankOpened?.Invoke(this, EventArgs.Empty);
        }
    }


    public class Dialog
    {
        public string ID;
        public List<string> textList;
        public List<DialogOption> options;
        public Dialog()
        {
            textList = new List<string>();
            options = new List<DialogOption>();
        }
    }

    public class DialogOption
    {
        public string NextMsgID;
        public string optiontext;
        public string Command;
        public string Condition;
    }
}
