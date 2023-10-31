import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import com.streambase.sb.Schema;
import com.streambase.sb.StreamBaseException;
import com.streambase.sb.Tuple;
import com.streambase.sb.operator.Operator;
import com.streambase.sb.operator.Parameterizable;
import com.streambase.sb.operator.TypecheckException;
import java.sql.*;

public class InsertOrder extends Operator implements Parameterizable {
	
	private Schema inSchema;
	private Schema outSchema;
	private Schema.Field[] fields;
	protected Connection conn;
	
	public InsertOrder () {
		//12 input fields and one output field.
		setPortHints(1,1);
	}
	
	@Override
	public void processTuple(int inputPort, Tuple t) throws StreamBaseException {
		//cria a tupla de saída.
		Tuple out = outSchema.createTuple();
				
		for (int ii  = 0; ii < fields.length; ii++) {
			out.setField(fields[ii], t.getField(fields[ii]));
		}
		
		DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		Calendar cal = Calendar.getInstance();
		
		int idSecurity, idBook, idSection, idExchange, idPessoa, idCorretora, idAccount, idSide; 
		double cash, price, quantity;
		idSecurity = (Integer) t.getField("IdSecurity");
		price = (Double) t.getField("Price");
		cash = (Double) t.getField("Cash");
		idBook = (Integer) t.getField("Id_Book");
		idSection = (Integer) t.getField("Id_Section");
		idExchange = (Integer) t.getField("IdExchange");
		idPessoa = (Integer) t.getField("Trader");
		idCorretora = (Integer) t.getField("Id_Corretora");
		idAccount = (Integer) t.getField("Id_Account");
		idSide = (Integer) t.getField("SideId");
		if (idSide == 1) {
			quantity = (Double) t.getField("Size");
		} else {
			quantity = -1*((Double) t.getField("Size"));
		}
		
		try {
			String sqlString;
			
			sqlString = "EXEC NESTDB.dbo.proc_insert_Tb012_Ordens "+idSecurity+", "+quantity+", "+price+", "+cash+", "+idBook+", "+idSection+", "+idExchange+", "+idPessoa+", '"+(dateFormat.format(cal.getTime()))+"', 1, "+idCorretora+", '"+(dateFormat.format(cal.getTime()))+"', "+idAccount+", 0, 0, "+idSide;
			
			System.out.println("Vou executar o comando:"+ sqlString);
					
			CallableStatement procStmt = null;
			Statement idStmt = null;
			ResultSet rs = null;
			
			procStmt = conn.prepareCall(sqlString);
			
			System.out.println(procStmt.executeUpdate());
			        
            sqlString = "SELECT @@IDENTITY AS IdOrderDB";
            
            idStmt = conn.createStatement();
            
            rs = idStmt.executeQuery(sqlString);
            
			while (rs.next()) {
				out.setInt("IdOrderDB", rs.getInt("IdOrderDB"));
				System.out.println(rs.getInt("IdOrderDB"));
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();		
		}
		sendOutput(0, out);		
	}

	@Override
	public void typecheck() throws PropertyTypecheckException,
		TypecheckException {
		inSchema = getInputSchema(0);
		fields = inSchema.getFields();
		outSchema = setOutputSchema(0, inSchema);
		getConnection(); 
	}
	
	public void getConnection () {
					
		//Verifica se o driver está disponível
		try
        {
			Class.forName("com.microsoft.jdbc.sqlserver.SQLServerDriver");
        } catch (java.lang.ClassNotFoundException e)
        {
        	//substituir posteriormente por um error output port
        	System.out.println("Driver nao disponivel");
        }
		//Estabelece a conexão com o banco de dados.
        try
        {
        	//String de conexão
        	this.conn = DriverManager.getConnection("jdbc:sqlserver://NESTSRV06;databasename=NESTDB;user=sa;password=Vende1000a10");
        	
        } catch (SQLException ex)
        {
        	System.out.println("Nao foi possivel estabelecer a conexao com o banco de dados");
        }			
	}
	
}
