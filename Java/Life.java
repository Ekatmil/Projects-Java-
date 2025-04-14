import java.util.Arrays;
import java.util.Scanner;

public class Life {
	static char left = Character.MIN_VALUE;
	static char right = Character.MIN_VALUE;
	static char up = Character.MIN_VALUE;
	static char down = Character.MIN_VALUE;
	static char diag1 = Character.MIN_VALUE;
	static char diag2 = Character.MIN_VALUE;
	static char diag3 = Character.MIN_VALUE;
	static char diag4 = Character.MIN_VALUE;

	public static void leftMove (char[][]ar, int i,int j) {
		if(i != 0) {
			left = ar[i-1][j];
			}
			else {
				left = ar[ar.length -1][j];
			}
	}
	public static void rightMove (char[][]ar, int i,int j) {
		if(i != ar.length-1) {
			right = ar[i+1][j];
			}
			else {
				right = ar[0][j];
			}
	}	
	public static void upMove (char[][]ar, int i,int j) {
		if(j != 0) {
			up = ar[i][j-1];
			rightMove(ar,i,j-1);
			diag1 = right;
			leftMove(ar,i,j-1);
			diag2 = left;
			}
			else {
				up = ar[i][ar.length -1];
				rightMove(ar,i,ar.length -1);
				diag1 = right;
				leftMove(ar,i,ar.length -1);
				diag2 = left;
			}
	}
	public static void downMove (char[][]ar, int i,int j) {
		if(j != ar.length-1) {
			down = ar[i][j+1];
			rightMove(ar,i,j+1);
			diag3 = right;
			leftMove(ar,i,j+1);
			diag4 = left;
			}
			else {
				up = ar[i][0];
				rightMove(ar,i,0);
				diag3 = right;
				leftMove(ar,i,0);
				diag4 = left;
			}
	}
		
	 public static void main(String[] args) 
	 {
		 int total = 0;
		 Scanner in = new Scanner(System.in);
		 String k = in.nextLine();
		 String[]s = k.split(" ");
		 int n = Integer.parseInt(s[0]);
		 int m = Integer.parseInt(s[1]);
		 char[][]a = new char[n][n];
		 char[][]a1 = new char[n][n];
		 for (int i = 0; i<n; i++) {
			 String l = in.nextLine();
			 char[]b = l.toCharArray();
			 for (int j = 0; j<n;j++) {
				 a[i][j] = b[j];
			 }
		 }
		 for (int l = 1; l <= m; l++) {
			 for (int i = 0; i<n; i++) {
				 for (int j = 0; j < n; j++) {
					 leftMove(a,i,j);
					 if (left == 'X') {
						 total +=1;
					 }
					 rightMove(a,i,j);
					 if (right == 'X') {
						 total +=1;
					 }
					 upMove(a,i,j);
					 if (up == 'X') {
						 total +=1;
					 }
					 if (diag1 == 'X') {
						 total +=1;
					 }
					 if (diag2 == 'X') {
						 total +=1;
					 }
					 downMove(a,i,j);
					 if (down == 'X') {
						 total +=1;
					 }
					 if (diag3 == 'X') {
						 total +=1;
					 }
					 if (diag4 == 'X') {
						 total +=1;
					 }
					 if (a[i][j] == 'X') {
						 if (total < 2 || total >3) {
							 a1[i][j] = '_';
						 }
						 else {
							 a1[i][j] = 'X';
						 }
					 }
					 else if (a[i][j] == '_') {
						 if (total == 3) {
							 a1[i][j] = 'X';
						 }
						 else {
							 a1[i][j] = '_';
						 }
					 }
					 total = 0;
				 }
			 }
			 a = Arrays.copyOf(a1, a1.length);
			 a1 = new char[n][n];
		 }
		 for (int i = 0; i < n; i++) {
			 for (int j = 0; j<n; j++) {
				 System.out.print(a[i][j]);
			 }
			 System. out. print("\n");
		 }
	 }
}
