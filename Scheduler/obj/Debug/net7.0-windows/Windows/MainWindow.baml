   M S B A M L   `   `   `  ����  	Scheduler    Scheduler.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  	Scheduler��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��700��5	   	   $
��1200��6   $	��700��5
   	   $
��1200��6    � Icon$ \Resources\winico.png��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ]� -   5        $��	MenuPanel��5      $��60��5      $��0��5         Mid gray#��[  5      .+�5      �� -   5        $��
HomeButton��5      $��60��5      $��60��6!   $
��Left=�5        Mid red#��[ 5        ReverseRoundedButtonStyle#��[ 5      .��5      �� $��/Resources/homeico.png��6   $��10q�68   5      ]� 5!      $��Center=�6   $��False��5"      .+�5$      �� -   5        $��TutionPageBttn��5$      $ ��Преподаватели��5&      #��[ 5'      
�������$��SemiBold"�5)      $��15(�5*      #��[ 5+      $��35��5,      $	��130��6%   $��15 10 10 10q�5-      �� 5/      -   5         ��Name$ CabinetPageBttn��5/      $��Кабинеты��51      #��[ 52      
�������$��SemiBold"�54      $��15(�55      #��[ 56      $��35��57      $��90��6%   $��10q�58      �� 5:      -   5        $��StudentGroupPageBttn��5:      $��Группы��5<      #��[ 5=      
�������$��SemiBold"�5?      $��15(�5@      #��[ 5A      $��35��5B      $��80��6%   $��10q�5C      �� 5E      -   5        $��SubjectPageBttn��5E      $��Предметы��5F   
   #��[ 5H      
�������$��SemiBold"�5J      $��15(�5K      #��[ 5L      $��35��5M      $	��100��6%   $��10q�5N      5P      �� 5R      -   5        $��UserNamePanel��5R       �� Orientation$ 
Horizontal=�5T      $��Right=�5U      $��10q�5V      .+�5X      �� -	   5        $��	UserImage��5X      $��/Resources/userico.png��5Y      $��5q�5Z      �� 5\      -
   5        $��UserNameTextBlock��5\      $��16(�5]      $
��Bold"�5^        	Text gray#��[ 5_      $��Center=�5`      5b      5d       � 5f   
   -   5        $��
PagesFrame��5f      $��1��5g        	Dark gray#��[ 5h        � NavigationUIVisibility$ Hidden=�5i      5j      5k      