using System;
using System.IO;
using System.Text;
using System.Data;

namespace TwgSendPTF
{
	public class clsArquivo
	{
		private static string m_sPathArquivoLog;
		private static string m_sNomeArquivoLog;

		/// <summary>
		/// Este metodo efetua a gravacao dos log do sistema em um arquivo TXT
		/// </summary>
		/// <param name="sMensagemParam">Mensagem que será gravada no arquivo</param>
		/// <param name="sClasseParam">Nome da Classe que provocou erro ou está enviando a MSG</param>
		/// <param name="sRotinaParam">Nome da Rotina que provocou erro ou está enviando a MSG</param>
		/// <param name="lPassoParam">Identificação dentro da rotina</param>
		public static void fnGravaLogTXT(string sMensagemParam) {
			
			// Crio variaveis para efetuar gravação no arquivo
			FileStream fsArqLog;
			StreamWriter swGravaLog;
			string sAuxi;
			DateTime dtLog = DateTime.Now;
			
			string sFile = pprPathArquivoLog+pprNomeArquivoLog;

			// Definio o tipo de abertura e demais parametros do arquivo de LOG
			fsArqLog = new FileStream(sFile,	// Path + Nome do arquivo
				FileMode.Append,	// Abre ou cria se nao existir
				FileAccess.Write,	// Somente gravacao no arquivo
				FileShare.None);	// Sem compartilhamento
		
			swGravaLog = new StreamWriter(fsArqLog, Encoding.Default);

			// Formata string para gravacao no arquivo
			sAuxi = "";
			sAuxi = string.Format(@"{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", 
				dtLog.Day, dtLog.Month, dtLog.Year, dtLog.Hour, dtLog.Minute, dtLog.Second) // Data e Hora
				+ " - " + sMensagemParam			// Mensagem que deve ser gravada no arquivo
				+ "\r\n";							// Salta linha

			// Grava no arquivo
			swGravaLog.Write(sAuxi);

			// Fecha a instacia do arquivo
			swGravaLog.Close();
			fsArqLog.Close();
		}

		/// <summary>
		/// Seta o nome do Arquivo onde será gravado os log's de erro gerenciado
		/// </summary>
		public static string pprNomeArquivoLog
		{
			set { m_sNomeArquivoLog = value;}
			get {return m_sNomeArquivoLog;}
		}

		/// <summary>
		/// Seta o caminho do Arquivo onde será gravado os log's de erro gerenciado
		/// </summary>
		public static string pprPathArquivoLog
		{ 
			set { m_sPathArquivoLog = value;} 
			get { return m_sPathArquivoLog;}
		}
	
	}
	
}
