import java.awt.Point;
import java.text.DecimalFormat;
import java.util.Scanner;

public  class Main {
	static double cur = Double.MAX_VALUE;
	static void swap(Point[] ar, int a, int b) {
		Point c;
	    c = ar[a];
	    ar[a] = ar[b];
	    ar[b] = c;
	}
	public static void permutation(int n, Point[] ar) 
	{
		if(n == 1) {
				double result = 0;
			for (int i = 0; i< ar.length - 1; i++) 
			{
				result= result + Math. sqrt((ar[i].x-ar[i+1].x)*(ar[i].x-ar[i+1].x) + (ar[i].y-ar[i+1].y)*(ar[i].y-ar[i+1].y));
			}
			if (result < cur) {
				cur = result;
			}
		}
			
		else { 
			for(int i = 0; i < n-1; i++) {
				permutation(n - 1, ar);
			    if(n % 2 == 0) {
			    	swap(ar, i, n-1);
			            }
			    else {
			    	swap(ar, 0, n-1);
			    }
			}
		
		permutation(n - 1, ar);
		}
	} 	 	
	
	 public static void main(String[] args) 
	 {
		 Scanner in = new Scanner(System.in);
		 String j = in.nextLine();
		 int n = Integer.parseInt(j);
		 Point[] a = new Point[n];
		 for(int i =0; i < n; i++)
		 {
			 String k = in.nextLine();
			 String[]s = k.split(" ");
			 int x = Integer.parseInt(s[0]);
			 int y = Integer.parseInt(s[1]);
			 a[i] = new Point(x,y);
		 }
		 permutation(n,a);
		 DecimalFormat formatter = new DecimalFormat("#.00"); 
		 String res = formatter.format(cur);
		 System.out.println("Minimal length of the network is: " + res);
		 in.close();
	 }
}
