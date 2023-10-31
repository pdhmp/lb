import com.streambase.sb.Schema;
import com.streambase.sb.StreamBaseException;
import com.streambase.sb.Tuple;
import com.streambase.sb.operator.Operator;
import com.streambase.sb.operator.Parameterizable;
import com.streambase.sb.operator.TypecheckException;
/*
 * This class receives in a tuple the order_id of the incoming order.
 * As output, sends {Id_Portfolio}, {Id_Book} and {Id_Section} as adittional fields in the 
 * output tuple.
 */

public class GetParameters extends Operator implements Parameterizable {
	
	private String displayName = "GetParameters";
	private Schema inSchema;
	private Schema outSchema;
	private Schema.Field[] fields;
	
	public GetParameters() {
		setPortHints(1,1);
		//setDisplayName(displayName);
		//setShortDisplayName(this.getClass().getSimpleName());
	}
	@Override
	public void processTuple(int arg0, Tuple t) throws StreamBaseException {
		
		Tuple out = outSchema.createTuple();
		
		for (int ii  = 0; ii < fields.length; ii++) {
			out.setField(fields[ii], t.getField(fields[ii]));
		}
		
		String orderId = (String) t.getField("AppOrderID");
		int Id_Portfolio, Id_Book, Id_Section;
		//Get Parameters
		Id_Portfolio = Integer.parseInt(orderId.substring(0,orderId.indexOf("_")));
		orderId = orderId.substring(orderId.indexOf("_")+1, orderId.length());
		Id_Book = Integer.parseInt(orderId.substring(0,orderId.indexOf("_")));
		orderId = orderId.substring(orderId.indexOf("_")+1, orderId.length());
		Id_Section = Integer.parseInt(orderId.substring(0,orderId.indexOf("_")));
		//Log Result
		System.out.println("IdPortfolio: "+Id_Portfolio+" Id_Book: "+Id_Book+" Id_Section: "+Id_Section);
		
		out.setInt("Id_Portfolio", Id_Portfolio);
		out.setInt("Id_Book", Id_Book);
		out.setInt("Id_Section", Id_Section);
		sendOutput(0, out);
	}

	@Override
	public void typecheck() throws PropertyTypecheckException,
			TypecheckException {
		inSchema = getInputSchema(0);
		fields = inSchema.getFields();
		outSchema = setOutputSchema(0, inSchema); 
	}
}
