# Aircraft Trajectories and Noise Visualization

Bachelor Graduation Project, Delft University of Technology<br>
Elvan Kula (ekula), E.Kula@student.tudelft.nl<br>
Hans Schouten (hansschouten), J.Schouten-1@student.tudelft.nl

----
Model-based Prediction and Visualization of Aircraft Noise
----

Aircraft and airport noise are complex subject matters which have been studied for decades and are still the focus of many research efforts nowadays. Also at the department of Air Transport & Operations at TU Delft’s Faculty of Aerospace Engineering.

The project team will be assigned to implement a program in C++ for the prediction and visualization of aircraft noise. More specifically, this will involve:

a)	The implementation of a mathematical model for aircraft noise and aircraft trajectory design. The model will be deployed by the project team (and later on by our research team) to predict aircraft noise along a particular trajectory (flight route) in order to be able to optimize the trajectory and to reduce the produced noise. In order to implement this model the parameters that are required for the sound calculation need to be derived mathematically.

b)	The code should be optimized with multi-core processing to achieve real-time performance.

c)	Besides that, a tool should be developed to visualize the noise produced along the simulated trajectory pictured on a real map. This requires an implementation of noise contours, which are ‘noise footprints’ whose shape indicate areas of constant noise. Noise contours are a new subject to our research group and haven’t been implemented before so this will be a challenging and novel topic.

The resulting program will present a creative and efficient way to compute and visualize aircraft noise along simulated and real flight routes.

[![Build status](https://ci.appveyor.com/api/projects/status/wptia5rpaq5c6b46?svg=true)](https://ci.appveyor.com/project/Hansschouten/aircraft-trajectories)
