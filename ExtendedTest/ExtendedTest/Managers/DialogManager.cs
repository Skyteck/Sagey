using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class DialogManager
    {
        public event EventHandler DialogPlayed;

        List<Dialog> _DialogList;
        public Dialog CurrentDialog;

        public DialogManager()
        {
            _DialogList = new List<Dialog>();
        }

        public void LoadDialog(string path)
        {
            var file = System.IO.File.ReadAllText(path);
            _DialogList = JsonConvert.DeserializeObject<List<Dialog>>(file);
        }

        public void PlayMessage(string msgID)
        {
            CurrentDialog = _DialogList.Find(x => x.ID == msgID);
            OnDialogPlayed();
        }

        public void OnDialogPlayed()
        {
            DialogPlayed?.Invoke(this, EventArgs.Empty);
        }
    }


    public class Dialog
    {
        public string ID;
        public List<string> textList;

        public Dialog()
        {
            textList = new List<string>();
        }
    }
}
