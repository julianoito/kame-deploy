using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    [Serializable]
    public abstract class IStepProcessor
    {
        #region Base Step parameters
        private string name;
        protected string workspace;
        //protected bool enabledUI = false;
        #endregion

        public abstract void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage);
        public abstract List<DeployFile> CheckExecution(Step step, DeployLog log);
        public abstract List<StepParameter> ListarParametrosUtilizados();

        protected void LoadParameters(Step step)
        {
            Parameter parameter;

            this.name = step.Name;

            try
            {
                parameter = step.GetParameter("workspace");
                if (parameter == null)
                {
                    this.ThrowAplicationException("O parametro connectionString não informado.");
                }
                this.workspace = parameter.ParameterValue;
            }
            catch {
                this.ThrowAplicationException("O parametro connectionString informado em um formata inválido");
            }
        
        }

		protected Encoding GetEncode(string file, out bool encodeDetected)
		{
			encodeDetected = false;
			Encoding encoding = Encoding.Default;

			byte[] allBytes = new byte[1024];//File.ReadAllBytes(caminho);

			FileStream fs = File.OpenRead(file);
			fs.Read(allBytes, 0, allBytes.Length);
			fs.Close();

			Mozilla.CharDet.UniversalDetector d = new Mozilla.CharDet.UniversalDetector();
			d.HandleData(allBytes);
			d.DataEnd();

			if (!string.IsNullOrEmpty(d.DetectedCharsetName))
			{
				encodeDetected = true;
				encoding = Encoding.GetEncoding(d.DetectedCharsetName);
			}

			return encoding;
		}

        protected string ReadTextFile(string caminho)
        {
            string conteudoArquivo = string.Empty;

            FileStream strm = null;
            StreamReader reader = null;
            try
            {
				bool encodeDetected = false;
				Encoding encoding = GetEncode(caminho, out encodeDetected);

                strm = File.OpenRead(caminho);
				reader = new StreamReader(strm, encoding, !encodeDetected);
                conteudoArquivo = reader.ReadToEnd();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (strm != null)
                {
                    strm.Close();
                }
            }

            return conteudoArquivo;
        }

        private Encoding DetectFileEnconding(string filename)
        {
			byte[] allBytes = File.ReadAllBytes(filename);
			Mozilla.CharDet.UniversalDetector d = new Mozilla.CharDet.UniversalDetector();
			d.HandleData(allBytes);
			d.DataEnd();

			if (string.IsNullOrEmpty(d.DetectedCharsetName))
			{
				return Encoding.Default;
			}
			else
			{
				return Encoding.GetEncoding(d.DetectedCharsetName);
			}

        }

        protected void ThrowAplicationException(string textoErro)
        {
            throw new ApplicationException("Erro ao executar o step " + this.name + ": " + textoErro);
        }

		protected void CopyFile(string sourceFile, string destinationFile, IProjectExecutionLog executionLog)
		{
			int array_length = (int)Math.Pow(2, 19);
			byte[] dataArray = new byte[array_length];

			if (File.Exists(destinationFile))
			{
				File.Delete(destinationFile);
			}

			FileStream fsread = null;
			BinaryReader bwread = null;
			FileStream fswrite = null;
			BinaryWriter bwwrite = null;

			string messagePrefix = "Copiado: ";
			if (executionLog != null)
			{
				executionLog.SetMessage("Iniciando copia do arquivo " + sourceFile, string.Empty);
			}

			try
			{
				long currentBytesCopied = 0,filesize = (new FileInfo(sourceFile)).Length;


				fsread = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.None, array_length);
				bwread = new BinaryReader(fsread);
				fswrite = new FileStream (destinationFile, FileMode.Create, FileAccess.Write, FileShare.None, array_length);
				bwwrite = new BinaryWriter(fswrite);

				for (; ; )
				{
					int read = bwread.Read(dataArray, 0, array_length);
					if (0 == read)
						break;

					bwwrite.Write(dataArray, 0, read);

					currentBytesCopied += read;
					if (executionLog != null)
					{
						executionLog.SetMessageFixedLine(
							messagePrefix + String.Format("{0:n2}", (Convert.ToDecimal(currentBytesCopied) / 1048576M)) + 
							" Mb de " + String.Format("{0:n2}", (Convert.ToDecimal(filesize) / 1048576M)) + " Mb"
							, messagePrefix
							, string.Empty);
					}
				}
			}
			catch { }
			finally
			{
				if (bwread != null)
				{
					bwread.Close();
				}

				if (fsread != null)
				{
					fsread.Close();
				}

				if (bwwrite != null)
				{
					bwwrite.Close();
				}

				if (fswrite != null)
				{
					fswrite.Close();
				}
			}
		}
    }
}
