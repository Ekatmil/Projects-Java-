package com.company;


import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Scanner;
import java.util.Stack;

//TEST

class Pair {
    public int first, second;
    public Pair(int first, int second) {
        this.first = first;
        this.second = second;
    }
}

class Game {
    String commands;
    int command_pointer = 0;
    char[] board;
    int pointer = 0;

    public Game(int length, String commands){
        this.board = new char[length];
        for (int i=0;i<this.board.length;i++)
            this.board[i] = (char)0;
        this.commands = commands;
    }

    void play() throws IOException {
        for (;this.command_pointer < this.commands.length();this.command_pointer++) {
            this.handle_command(this.commands.charAt(this.command_pointer));
        }
    }

    boolean check_string(String s) {
        boolean result = true;
        Stack<Pair> stack = new Stack<>();
        int line = 1;
        int char_number = 1;
        for (char c : s.toCharArray()) {
            if (c == '\n') {
                line++;
                char_number = 1;
                continue;
            } else if (c == '['){
                stack.push(new Pair(line, char_number));
            } else if (c == ']') {
                if (stack.isEmpty()) {
                    //char_number -=1;
                    System.out.println("Unopened cycle - line " + line + " character " + char_number);
                    //return false;
                    result = false;
                } else
                    stack.pop();
            }
            char_number++;
        }
        if (!stack.isEmpty()){
            for (Pair p : stack) {
                System.out.println("Unclosed cycle - line "+p.first+" character " + p.second);
                //return false;
            }
            result =  false;
        }

        return result;
    }

    void handle_command(char c) throws IOException {
        int depth = 0;
        switch (c) {
            case '>':
                if ((this.pointer + 1) >= this.board.length) {
                    this.pointer = 0;
                    //System.out.println("Memory overrun");
                    //System.exit(0);
                } else {
                    this.pointer++;
                }
                break;
            case '<':
                if ((this.pointer - 1) < 0) {
                    //this.pointer = this.board.length-1;
                    System.out.println("Memory underrun");
                    System.exit(0);
                } else {
                    this.pointer--;
                }
                break;
            case '+':
                this.board[this.pointer]++;
                break;
            case '-':
                this.board[this.pointer]--;
                break;
            case '.':
                System.out.print(this.board[this.pointer]);
                break;
            case ',':
                //Scanner scanner = new Scanner(System.in);
                //System.out.println("Accepting input:");
                char in = (char) System.in.read();
                this.board[this.pointer] = in;
                break;
            case '[':
                if (this.board[this.pointer] == 0) {
                    this.command_pointer++;
                    while (depth > 0 || this.commands.charAt(this.command_pointer) != ']') {
                        if (this.commands.charAt(this.command_pointer) == '[')
                            depth++;
                        else if (this.commands.charAt(this.command_pointer) == ']')
                            depth--;
                        this.command_pointer++;
                    }
                    //do {
                    //    this.command_pointer++;
                    //} while (this.commands.charAt(this.command_pointer) != ']');
                    //this.command_pointer++;
                }
                break;
            case ']':
                if (this.board[this.pointer] != 0) {
                    this.command_pointer--;
                    while (depth > 0 || this.commands.charAt(this.command_pointer) != '[') {
                        if (this.commands.charAt(this.command_pointer) == ']')
                            depth++;
                        else if (this.commands.charAt(this.command_pointer) == '[')
                            depth--;
                        this.command_pointer--;
                    }
                    //do {
                    //    this.command_pointer--;
                    //} while (this.commands.charAt(this.command_pointer) != '[');
                }
                break;

        }
    }

}

public class Main {
    public static void main(String[] args){
        String file_name = args[0];
        int length = 30000;
        if (args.length > 1)
            length = Integer.parseInt(args[1]);
        File file = new File(file_name);
        try (Scanner scanner = new Scanner(file)) {
            String content = "";
            String commands = "";
            while (scanner.hasNextLine()) {
                String line = scanner.nextLine();
                content += line + "\n";
                for (char c : line.toCharArray()) {
                    commands += c;
                }
            }

            Game g = new Game(length, commands);
            if (g.check_string(content))
                g.play();
        } catch (IOException e) {}

    }
}
