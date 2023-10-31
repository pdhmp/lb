package com.Nest.NestProject;

import java.util.List;
import com.streambase.sb.CompleteDataType;
import com.streambase.sb.client.CustomFunctionResolver;

public class Math {
	@CustomFunctionResolver("percentileCustomFunctionResolver0")
	public static double percentile(List<?> Range, double Percentile) {
		
		double count = Range.size();
		
		for (int ii = 0; ii < Range.size(); ii++)
		{
			if (((ii+1)/count) >= Percentile)
			{
				try
				{
					return Double.parseDouble(Range.get(ii).toString());
				} catch (NumberFormatException nfe) 
				{
					//Trying to convert string into double. I can have a lot of trouble doing such a thing.
					System.out.println("Error trying to convert string to double. In: com.Nest.NestProject -> Math.java -> percentile simple function.");
				}
			}
		}
		
		return 0.0;
	}
	public static CompleteDataType percentileCustomFunctionResolver0(
			CompleteDataType Range, CompleteDataType Percentile) {
		// TODO: Implement custom function resolver functionality here
		
		return CompleteDataType.forDouble();
	}
	/**
	* A StreamBase Simple Function. Use this function
	* in StreamBase expressions using the <em>calljava</em> function, or 
	* by an assigned alias. It can then be called directly 
	* using the alias name instead of using calljava().
	*/
	@CustomFunctionResolver("elementCounterCustomFunctionResolver0")
	public static int elementCounter(List<?> arg0, int arg1){
	    // TODO Implement function here
		int sum = 0;
		for (int jj = 0; jj < arg0.size(); jj++)
		{
			try
			{
				if (Integer.parseInt(arg0.get(jj).toString()) == arg1)
				{
					sum+=Integer.parseInt(arg0.get(jj).toString());
				}
			} catch (NumberFormatException nfeInt)
			{
				System.out.println("Error trying to convert string to integer. In: com.Nest.NestProject -> Math.java -> elementCounter simple function.");
			}
		}
	    return sum;
	}
	/**
	* A StreamBase Custom Function Resolver Function. This method is used by 
	* StreamBase for type checking.  Use this method
	* to resolve functions that take lists or tuples as arguments 
	* or return lists or tuples.  This method should be used to ensure the 
	* data types of your function are correct.
	
	* For custom functions that return simple types (the Java equivalents of the simple 
	* StreamBase types: int, long, double, bool, string, blob, timestamp), the returned CompleteDataType
	* must be the appropriate corresponding simple type (e.g., as returned by CompleteDataType.forInt()).
	* For custom functions that return non-simple types (the Java equivalents of the non-simple
	* StreamBase types: tuple, list), the returned CompleteDataType must fully describe the appropriate
	* non-simple type (as returned by CompleteDataType.forTuple(Schema) or CompleteDataType.forList(CompleteDataType))
	*{@see com.streambase.sb.client.CustomFunctionResolver}  
	*/
	public static CompleteDataType elementCounterCustomFunctionResolver0(CompleteDataType arg0, CompleteDataType arg1) {
	    // TODO: Implement custom function resolver functionality here
	
	return CompleteDataType.forInt();
	}
	
	

}
