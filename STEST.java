import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Main{
    static int[] Point = new int[100];
    static int index = 0;

    public static int FinalPoints(Map<Integer, ArrayList> questions, Map<Integer, ArrayList> answers){
        int points = 0;
        int pointsforEach = 0;
        for (Map.Entry<Integer,ArrayList> EachQuestion : questions.entrySet()){
            int num = 0;
            int k = EachQuestion.getKey();
            ArrayList<String> values = EachQuestion.getValue();
            for (Map.Entry<Integer,ArrayList> EachAnswer : answers.entrySet()) {
                int no = EachAnswer.getKey();
                ArrayList<String> answer = EachAnswer.getValue();
                if (no == k) {
                    ArrayList<String> values1 = new ArrayList<String>(values);
                    ArrayList<String> answer1 = new ArrayList<String>(answer);
                    values1.removeAll(answer1);
                    points += values1.size();
                    pointsforEach += values1.size();
                    answer.removeAll(values);
                    points += answer.size();
                    pointsforEach += answer.size();
                    Point[k] = pointsforEach;
                    pointsforEach = 0;
                    index = k;
                    num = 1;
                    break;
                }
                if (no > k ) {
                    points += values.size();
                    pointsforEach = values.size();
                    Point[k]  = pointsforEach;
                    pointsforEach = 0;
                    index = k;
                    num = 1;
                    break;
                }
            }
            if (num == 0){
                points += values.size();
                pointsforEach = values.size();
                Point[k]  = pointsforEach;
                pointsforEach = 0;
                index = k;
                break;
            }
        }
        return points;
    }
    public static void PrintOutput(int[]Point, String person, int result){
        System.out.println(person);
        for (int i = 1; i <= index; i++){
            System.out.println(i + ". " + Point[i]);
        }

        if (result <= 2) System.out.println("Result: 1 (" + result + ')');
        if (2< result && result <= 5) System.out.println("Result: 2 (" + result + ')');
        if (5< result && result <= 8) System.out.println("Result: 3 (" + result + ')');
        if (9<= result) System.out.println("Result: 4 (" + result + ')');
    }

    public static void main(String[] args) {
        Map<Integer, ArrayList> questions = new HashMap<Integer, ArrayList>();
        Map<Integer, ArrayList> answers = new HashMap<Integer, ArrayList>();
        Scanner in = new Scanner(System.in);
        String current = in.nextLine();
        String name = null;
        while (current != null) {
            if (current.equals(""))
                current = in.nextLine();
            ArrayList<String> arrlist = new ArrayList<String>();
            if (current.equals("multichoice") || current.equals("singlechoice")) {
                current = in.nextLine();
                String currentNew = current.replaceAll("\\.", "");
                String[] s = currentNew.split("\\s+");
                int a = Integer.valueOf(s[0]);
                while (true) {
                    current = in.nextLine();
                    if (current.contains("Answer: ")) {
                        break;
                    }
                }
                String[] l = current.split("\\s+");
                for (int i = 1; i < l.length; i++) {
                    arrlist.add(l[i]);
                }
                questions.put(a, arrlist);
                if (in.hasNextLine())
                    current = in.nextLine();
                else
                    break;
            }
            else if (!current.equals("")) {
                name = current;
                current = in.nextLine();
                while (current != null) {
                    if(!current.equals("")) {
                        ArrayList<String> arrlist1 = new ArrayList<String>();
                        String currentNew = current.replaceAll("\\.", "");
                        String[] s = currentNew.split("\\s+");
                        int a = Integer.valueOf(s[0]);
                        for (int i = 1; i < s.length; i++) {
                            arrlist1.add(s[i]);
                        }
                        answers.put(a, arrlist1);
                    }
                    if (in.hasNextLine()) {
                        current = in.nextLine();
                        if (current.equals("")) break;
                    }
                    else
                        break;
                }
                int result = FinalPoints(questions, answers);
                PrintOutput(Point, name, result);
            }
            if (!in.hasNextLine())
                break;
        }
    }
}
