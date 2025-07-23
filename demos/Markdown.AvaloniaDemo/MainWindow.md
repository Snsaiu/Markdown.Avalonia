好的，这里提供一个封装了 NPOI 工作表顺序设置功能的 C# 类。这个类会处理工作簿的打开、创建、添加工作表、设置顺序以及保存。它还实现了 `IDisposable` 接口，以确保资源被正确释放。
$$
\int_0^1 \frac{1}{1 + x^2} \, dx = \arctan(1) - \arctan(0) = \frac{\pi}{4}
$$


### Link ###
Links [Go to Google!](https://www.google.com)  
Links with title [Go to Google!](https://www.google.com "google.")  
Links with image [![faviicon](https://www.google.com/favicon.ico)](https://www.google.com "google favicon")

![localimage](LocalPath.png)
![ResourceImage](Assets/ResourceImage.png)
![Svg](Assets/Vector.svg)

$$
\int_0^1 x^2 \, dx = \frac{1}{3}
$$

$$
\sum_{n=1}^{\infty} \frac{1}{n^2} = \frac{\pi^2}{6}
$$

$$
\nabla \cdot \vec{E} = \frac{\rho}{\varepsilon_0}
$$

$$
f(x) = \begin{cases}
x^2 & x \geq 0 \\
-x & x < 0
\end{cases}
$$

$$
\left( \frac{a+b}{c+d} \right)^2
$$

$$
A = \begin{bmatrix}
a_{11} & a_{12} \\
a_{21} & a_{22}
\end{bmatrix}
$$

这是一个简单的表达式 $x + y = z$，再看一个平方公式 $a^2 + b^2 = c^2$。

一些带分数的表达式：$\frac{1}{2} + \frac{1}{3} = \frac{5}{6}$。

指数表达式：$e^{i\pi} + 1 = 0$，对数表达式：$\log_b a = c$。

还有向量点乘：$\vec{a} \cdot \vec{b} = ab\cos\theta$。

希腊字母：$\alpha + \beta = \gamma$。



### Text decolation [included original enhance] ###
*italic*, **bold**, ***bold-italic***, ~~strikethrough~~, __underline__ and %{color:red}color%.  
%{color:blue}***~~__Mixing Text__~~***%

### Link ###
Links [Go to Google!](https://www.google.com)  
Links with title [Go to Google!](https://www.google.com "google.")  
Links with image [![faviicon](https://www.google.com/favicon.ico)](https://www.google.com "google favicon")

### Image ###
#### Remote images ####
![image1](https://github.com/whistyun/Markdown.Avalonia/raw/master/docs/img.demo/scrn1.png)
#### Local and resource images ####
![localimage](LocalPath.png)
![ResourceImage](Assets/ResourceImage.png)
![Svg](Assets/Vector.svg)

### List ###
#### ul
* one
* two

#### ol
1. one
2. two
#### alphabet-ol [original enhance]
a. one
b. two

#### roman-ol [original enhance]
i, one
ii, two

### Table [included original enhance] ###
|a|b|c|d|
|:-:|:-|-:|
|a1234567890|b1234567890|c1234567890|d1234567890|
|a|/2.b|c|d|
|A|\2.C|
|1|2|3|4|
|あ|い|う|え|

### Code ###
Markdown.Xaml support ```inline code ``` and block code.
```c
#include <stdio.h>
int main()
{
   // printf() displays the string inside quotation
   printf("Hello, World!");
   return 0;
}
```

### Note ###

< notetext >

<p>. notetext


### Separator ###
single line
---
two lines
===
bold line
 ***
bold with single
___

### Blockquote ###
> ## Features ##
> MarkDown.Xaml has a number of convenient features
>
> * The engine itself is a single file, for easy inclusion in your own projects
> * Code for the engine is structured in the same manner as the original MarkdownSharp
> * Includes a `TextToFlowDocumentConverter` to make it easy to bind Markdown text

### Text Alignment [original enhance] ###
MdXaml parse a head of paragraph. If 'p[<=>].' is found, apply text alignment to it.
> p<. left alignment text
>
> p>. right alignment text
>
> p=. center alignment


## What is this Demo? ##

This demo application shows MdXaml in use - as you make changes to the
left pane, the rendered MarkDown will appear in the right pane.

### Source ###

Review the source for this demo to see how MdXaml works in practice, how to use the MarkdownScrollViewer,
and how to style the output to appear the way you desire.
