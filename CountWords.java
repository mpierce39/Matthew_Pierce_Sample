import javax.swing.JOptionPane;


public class CountWords 
{

	public static void main(String[] args) 
	{
		String inputtedString;
		int stringLength;
		int index;
		char c;
		int wordCount;
		inputtedString = JOptionPane.showInputDialog(null,"Please Enter your First name");
		stringLength = inputtedString.length();
		wordCount = 0;
		for(index = 0; index < stringLength; index ++)
		{
			
			c = inputtedString.charAt(index);
			if(c == '?')
			{
				wordCount = 0;
				
			}
			else if (c == '.')
			{
				
				wordCount = 0;
			}
			else if(c == ' ')
			{
				wordCount = 0;
			}
			else 
			{
				wordCount = index ++;
			}
			JOptionPane.showMessageDialog(null, "You Inputted " + inputtedString + "\nThat is "+ wordCount + "Words");
		}
	}
	
	

}
