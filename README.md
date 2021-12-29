<div align="center">
<h1 align="center">Unity Interpolation System</h1>

  <p align="center">
    A Unity system that interpolates joints using three methods of interpolation.
    <br />
    <a href="https://youtu.be/czeXk-9Afak">Full Demo</a>
  </p>
</div>

## About The Project

<div align="center">
<img src="https://i.imgur.com/GkwqG7F.gif" alt="sphere-interpolation" width="75%"/>
<p>(from left to right: LinEuler, Slerp, Squad)</p>
</div>


<br>


This project is made for my Master's thesis: [**A Comparison between Different Interpolation Methods for Character Upper Body Animation**](https://hammer.purdue.edu/articles/thesis/A_COMPARISON_OF_INTERPOLATION_METHODS_FOR_VIRTUAL_CHARACTER_UPPER_BODY_ANIMATION/13341461) at Purdue University.

The system can be used to generate character animation using three common interpolations: 
Linear Euler (LinEuler), Spherical Linear Quaternion (Slerp) and Spherical Spline Quaternion (Squad). 

Given any number of keyframes, the system will generate in-between keyframes using the corresponding algorithms. The mathematical equations, motion curves and velocity graphs are well studied. My thesis focuses on the perceptional aspect of evaluating these interpolations on upper-body character animation, as upper-body movements are more expressive. Four types of body gestures will be recorded using motion capture technology and to be extracted into limited keyframes. These keyframes will then populate as the animation stimuli.

An playlist used for my pilot study can be found [here](https://www.youtube.com/playlist?list=PLsyUQwBMw6ZIIrFwQBfxO3OyhxCFRlWAr)
and for my main study [here](https://www.youtube.com/playlist?list=PLsyUQwBMw6ZKN3ATNf_bm-Cv_HPIycZ0S)

## Publication

This result of this project is also published at ISVC 2021 Advances in Visual Computing: [**Perceived Naturalness of Interpolation Methods for Character Upper Body Animation**](https://link.springer.com/chapter/10.1007/978-3-030-90439-5_9)

### Abstract

We compare the perceived naturalness of character animations generated using three interpolation methods: linear Euler, spherical linear quaternion, and spherical spline quaternion. While previous work focused on the mathematical description of these interpolation types, our work studies the perceptual evaluation of animated upper body character gestures generated using these interpolations. Ninety-seven participants watched 12 animation clips of a character performing four different upper body motions: a beat gesture, a deictic gesture, an iconic gesture, and a metaphoric gesture. Three animation clips were generated for each gesture using the three interpolation methods. The participants rated their naturalness on a 5-point Likert scale. The results showed that animations generated using spherical spline quaternion interpolation were perceived as significantly more natural than those generated using the other two interpolation methods. The findings held true for all subjects regardless of gender and animation experience and across all four gestures.

