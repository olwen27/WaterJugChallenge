# Algorithmic Approach: Briefly explain the mathematical theory or algorithm used to find the solution.
 1.  Initialization:
        - Initialize variables, including the current values of BucketX (currentX) and BucketY (currentY).
        - I Set a boolean flag start to track the first iteration.
        - Create an empty list (waterJugChallenge) to store the steps taken.

 2. Simulation Loop: 
    - I Use a while loop to iterate until either BucketX or BucketY reach the desired amount of water (TargetAmount) which make the following things:
        - Set currentX to the capacity of BucketX.
        - If it is the first iteration, add a step to the list indicating that Bucket X is filled.
        - Transfer Water from Bucket X to Bucket Y.
        - Calculate the amount that can be transferred (transferAmount) without overflowing BucketY.
        - Update the current levels of water in both buckets based on the transfer.
        - Add a step to the list indicating the transfer from Bucket X to Bucket Y
        - Check if the desired amount is reached after the transfer. If that is the case return the list of steps.
        - If Bucket X is empty after the transfer, refill it to its capacity.
        - Add a step to the list indicating the refill.
        - Check if the goal is reached after the refill. If so, return the list of steps.
        - The loop continues until the goal is reached in either BucketX or BucketY.
        - If the loop exits without reaching the goal, return the list of steps indicating the progress made so far.

# Instruction: To run the program:
 1. Clone project from GitHub repository:
    - Open your preferred terminal or command prompt
    - On your command navigate to the directory where you want to clone the project for example cd path/to/desired/directory .
    - Run the following command to clone the repository git clone https://github.com/olwen27/WaterJugChallenge.git
    
 2. Open the project
    - Open Visual Studio.
    - Click on "Open a project or solution.
    - Navigate to the folder where you cloned the project.
    - Click on the file named "WaterJugChallenge.sln" to open the solution.
 
 3. How to run the project:
    - Press F5 to run the project.
 
 4. how to use the API: 
     - Endpoint Water Jug Challenge: on this endpoint you will find 3 inputs that will be use to to perform the challenge 3 of them are require.
     - Validations:
        - BucketX, BucketY and TargetAmount are greater than 0.
        - TatgetAmount it not greater than BucketX and BucketY.
        - Eather or numbers are even or odd.
     - Results:
       - If BucketX is the closest value to the target amount it will start by filling BucketX.
       - If BucketY is the closest value to the target amount it will start by filling BucketY.
       - in case there is any mismatch durring the process it will return the progress at the moment.
