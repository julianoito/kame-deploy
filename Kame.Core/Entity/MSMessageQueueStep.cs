using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    public class MSMessageQueueStep : IStepProcessor
    {
        private string operation;
        private bool ignorePermissionError;
        private Dictionary<string, string> queueList;
        private List<string> permissionUserList;
        private string accessRights;

        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            LoadParameters(step);
            errorMessage = string.Empty;

            MessageQueueAccessRights messageRithg = 0;
            switch (this.accessRights.ToUpper().Trim())
            { 
                case "FULLCONTROL":
                    messageRithg = MessageQueueAccessRights.FullControl;
                    break;
                case "WRITEMESSAGE":
                    messageRithg = MessageQueueAccessRights.WriteMessage;
                    break;
                case "RECEIVEMESSAGE":
                    messageRithg = MessageQueueAccessRights.ReceiveMessage;
                    break;
                case "PEEKMESSAGE":
                    messageRithg = MessageQueueAccessRights.PeekMessage;
                    break;
                case "DELETEMESSAGE":
                    messageRithg = MessageQueueAccessRights.DeleteMessage;
                    break;
                default:
                    messageRithg = 0;
                    break;
            }

            foreach (KeyValuePair<string, string> messageQueue in queueList)
            {
                if (executionLog != null)
                {
                    executionLog.SetMessage("Executando step " + step.Name + ": processando fila " + messageQueue.Key, string.Empty);
                }
                switch (this.operation.ToUpper())
                {
                    case "CREATE":
                        if (!System.Messaging.MessageQueue.Exists(messageQueue.Key))
                        {
                            MessageQueue queue = MessageQueue.Create(messageQueue.Key, true);

                            if (messageRithg > 0)
                            {
                                foreach (string user in permissionUserList)
                                {
                                    try
                                    {
                                        queue.SetPermissions(user.Trim(), messageRithg, AccessControlEntryType.Allow);
                                    }
                                    catch (Exception ex)
                                    {
                                        if (!this.ignorePermissionError)
                                        {
                                            throw ex;
                                        }
                                    }

                                }
                            }

                            if (!string.IsNullOrEmpty(messageQueue.Value.Trim()))
                            {
                                queue.Label = messageQueue.Value.Trim();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public override List<DeployFile> CheckExecution(Step step, DeployLog log)
        {
            return new List<DeployFile>();
        }

        public override List<StepParameter> ListarParametrosUtilizados()
        {
            List<StepParameter> listaParametros = new List<StepParameter>();


            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "operation"
                    , string.Empty
                    , "Operação a ser realizada no MS Message Queue. CREATE- Criar fila")
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "ignorePermissionError"
                    , string.Empty
                    , "Ignora o erro de atribuição de permissões da fila (true ou false)")
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "messageQueues"
                    , string.Empty
                    , "Lista de filas de mensagem separados por\";\". Caso a fila tenha um caminho e um label as duas informações devem ser separadas por \"|\"")
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "permissionUserList"
                    , string.Empty
                    , "Lista de usuários que receberão vinculados a fila, separados por \";\"")
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "accessRights"
                    , string.Empty
                    , "Tipos de acesso que será vinculado aos usuários. (FullControl, WriteMessage, ReceiveMessage, PeekMessage, DeleteMessage)")
            );

            return listaParametros;
        }

        protected virtual void LoadParameters(Step step)
        {
            Parameter parametro;
            try
            {
                parametro = step.GetParameter("operation");
                this.operation = parametro.ParameterValue;
            }
            catch
            {

            }

            try
            {
                parametro = step.GetParameter("ignorePermissionError");
                this.ignorePermissionError = bool.Parse(parametro.ParameterValue);
            }
            catch
            {

            }

            queueList = new Dictionary<string, string>();
            try
            {
                parametro = step.GetParameter("messageQueues");

                string[] messageQueues = parametro.ParameterValue.Split(';');
                foreach (string messageQueue in messageQueues)
                {
                    string queuePath = string.Empty, queueLabel = string.Empty;

                    if (messageQueue.Trim() != string.Empty)
                    {
                        if (messageQueue.Contains('|'))
                        {
                            string[] queueData = messageQueue.Split('|');
                            queuePath = queueData[0];
                            queueLabel = queueData[1];
                        }
                        else
                        {
                            queuePath = messageQueue;
                        }
                        queueList.Add(queuePath, queueLabel);
                    }
                }
            }
            catch
            {

            }

            this.permissionUserList = new List<string>();
            try
            {
                parametro = step.GetParameter("permissionUserList");
                this.permissionUserList.AddRange(parametro.ParameterValue.Split(';'));
            }
            catch
            {

            }

            try
            {
                parametro = step.GetParameter("accessRights");
                this.accessRights = parametro.ParameterValue;
            }
            catch
            {

            }

        }
    }
}
