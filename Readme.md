# ITransition Task #3


When launched with command line parameters (arguments to the main or Main method in the case of Java or C#, sys.argv in Python, process.argv in Node.js, etc.) it accepts an odd number ≥ 3 non-repeating strings (if the arguments are incorrect, you must display a neat error message—what exactly is wrong and an example of how to do it right). All messages should be in English. These passed strings are moves (for example, Rock Paper Scissors or Rock Paper Scissors Lizard Spock or 1 2 3 4 5 6 7 8 9).

Important: moves are passed as command line arguments, you don't parse them from the input stream (for example, a move may contain a space, but it shouldn't matter to your code).

The victory is defined as follows—half of the next moves in the circle wins, half of the previous moves in the circle lose (the semantics of the strings-moves is not important, he plays by the rules build upon the moves order the user used, even if the stone loses to scissors in its order—the contents of the strings-moves are not important for you).

The script generates a cryptographically strong random key (SecureRandom, RandomNumberGenerator, etc. - mandatory) with a length of at least 256 bits, makes own (computer's) move, calculates HMAC (based on SHA2 or SHA3) from the own move as a message with the generated key, displays the HMAC to the user. After that the user gets "menu" 1 - Stone, 2 - Scissors, ...., 0 - Exit. The user makes his choice (in case of incorrect input, the "menu" is displayed again). The script shows who won, the move of the computer and the original key.

When you select the "help" option in the terminal, you need to display a table (ASCII-graphic) that determines which move wins.