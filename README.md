# DNA.XForms
DNA.XForms is an open source suite of Xamarin.Forms controls and services brought to you by Digital Nomad Apps.  Controls which are included (or plan to be included) are: Vector Speech Bubble, Image Speech Bubble, Rounded Frame, and a set of Drag and Drop controls 

The library is in ongoing development, but the following controls are already usable:
+ CappedImage (iOS only) - A subclass of the Xamarin.Forms Image control which allows specifying a scalable area in an image so that the image can be scaled up or down.  For example a button with a very particular look, or a speech-bubble where scaling the image needs to keep the arrow the same size. 
+ RoundedFrame (iOS only) - Similar to the Xamarin.Forms Frame control, but allows specifying a corner radius to customize the appearance
+ VectorSpeechBubble (iOS only) - A speech bubble control where the appearance is created through vector drawing.  The arrow direction, colors, borders, corner radius, and shadow effects are all customizable. 

<h2>Getting Started</h2>
The controls all have sample implementations in the DNA.XForms.Sample project, so you're best off looking at this first.

<h2>Roadmap</h2>

1. Create Android implementations for RoundedFrame, CappedImage, VectorSpeechBubble
2. Complete the ImageSpeechBubble implementation for iOS first, then Android
3. Drag and Drop controls for iOS first, and then Android
4. Create XAML Samples

<h2>Get Involved</h2>
I will gratefully accept pull requests for bug fixes as well as for Windows Phone implementations of any of the controls.  
I have no plans at this stage to create Windows Phones implementations myself, but if someone else wants to, I will gladly accept.   

<h2>License</h2>
DNA.XForms is licensed under the MIT License.  Read more about it in the <a href="">LICENSE</a> file.

The MIT License requires proper attribution.  If you wish to use the controls without attribution, please contact matt@digitalnomadapps.co to discuss alternative licensing options.

