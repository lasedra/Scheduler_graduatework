   M S B A M L   `   `   `  ����   	Scheduler%    Scheduler.Pages.UserProfilePage   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  UserProfilePage�� � 	Resources  �� 
TargetType   ReadOnlyTextBoxStyle �� Triggers P� Property P� Value �� Property  	Text gray �� Value��%�   &	        7  �� " �� P�  �@��
IsReadOnly$
 True��. ���  � ��8   P�  �@��
IsReadOnly$ False��. ���  � ��
 �����.��� $��300 70 300 70q� � RowDefinitions �� $
��Auto ��� .+�]� $��5q�$��Center=�$��Center=�$��0��.+��� -   	 ��Name$	 RegisterProfilePic��$ ��/Resources/registerico.png��$y�	Collapsed=�$��5q�$��50��$��50���� -   $	 UserProfilePic��$��/Resources/profileico.png��$��5q�$��50��$��50���� -   $��ProfileNameTextBlock��$��WrapWithOverflow=�$��Center=�$��5q�#��[ $��24(�$
��Bold"��� $��1��$��	50 0 50 0q�$��Center=�.+�]� .+��� $��Имя:��$��Center=�$��5q�#��[ $��14(�$
��Bold"�$
��Left=��� -   $��EditNameBttn��$��/Resources/editico.png��$��20��$��20��$��0 0 5 0q�$��Right=��� -   $��NameDecision��$y�	Collapsed=�
 �� Orientation$
 
Horizontal=�$��0 0 5 0q�$��Right=�.+��� -   $��NameAcceptBttn��$��/Resources/acceptico.png��$��20��$��20���� -   $��NameDeclineBttn��$��/Resources/declineico.png��$��20��$��20���� -	   $��NameTextBox�� �� 
IsReadOnly$
 True��$��Center=�$��5q�#��[  ]� .+��� $��Логин:��$��Center=�$��5q�#��[ $��14(�$
��Bold"�$
��Left=��� -
   $��EditLoginBttn��$��/Resources/editico.png��$��20��$��20��$��0 0 5 0q�$��Right=��� -   $��LoginDecision��$y�	Collapsed=�$
 
Horizontal=�$��0 0 5 0q�$��Right=�.+��� -   $��LoginAcceptBttn��$��/Resources/acceptico.png��$��20��$��20���� -   $��LoginDeclineBttn��$��/Resources/declineico.png��$��20��$��20���� -   $��LoginTextBox��$
 True��$��Center=�$��5q�#��[  ]� .+��� $��Пароль:��$��Center=�$��5q�#��[ $��14(�$
��Bold"�$
��Left=��� -   $��EditPasswordBttn��$��/Resources/editico.png��$��20��$��20��$��0 0 5 0q�$��Right=��� -   $��PasswordDecision��$y�	Collapsed=�$
 
Horizontal=�$��0 0 5 0q�$��Right=�.+��� -   $��PasswordAcceptBttn��$��/Resources/acceptico.png��$��20��$��20���� -   $��PasswordDeclineBttn��$��/Resources/declineico.png��$��20��$��20���� -   $��PasswordTextBox��$
 True��$��Center=�$��5q�#��[  ]� -    ]�Name$ EditRolePanel��.+��� $
 
Horizontal=�$��Center=�.+�	� -   $��ManagerRadioBttn��$��Менеджер��$��5q�$��Center=�#��[ $��14(�$
��Bold"�	� -   $��TutorRadioBttn��$ ��Преподаватель��$��5q�$��Center=�#��[ $��14(�$
��Bold"�]� .+��� $��Тел. номер:��$��Center=�$��5q�#��[ $��14(�$
��Bold"�$
��Left=��� -   $��EditPhoneBttn��$��/Resources/editico.png��$��20��$��20��$��0 0 5 0q�$��Right=��� -   $��PhoneDecision��$y�	Collapsed=�$
 
Horizontal=�$��0 0 5 0q�$��Right=�.+��� -   $��PhoneAcceptBttn��$��/Resources/acceptico.png��$��20��$��20���� -   $��PhoneDeclineBttn��$��/Resources/declineico.png��$��20��$��20���� -   $��PhoneTextBox��$
 True��$��Center=�$��5q�#��[  ]� .+��� $��E-mail:��$��Center=�$��5q�#��[ $��14(�$
��Bold"�$
��Left=��� -   $��EditEmailBttn��$��/Resources/editico.png��$��20��$��0 0 5 0q�$��20��$��Right=��� -   $��EmailDecision��$y�	Collapsed=�$
 
Horizontal=�$��0 0 5 0q�$��Right=�.+��� -   $��EmailAcceptBttn��$��/Resources/acceptico.png��$��20��$��20���� -   $��EmailDeclineBttn��$��/Resources/declineico.png��$��20��$��20���� -    $��EmailTextBox��$
 True��$��Center=�$��5q�#��[  �� -!   $��
LogOutBttn��$&�� Выйти из аккаунта��$��30��$��5 15 5 5q�  RoundedButtonStyle#��[   	Light red#��[ �� -"    ��Name$ DeleteUserBttn��$y�	Collapsed=�$��1��$��Удалить��$��5 15 5 5q�$��30��#��[   Dark red#��[ 
��������� -#   $��CreateAccountBttn��$y�	Collapsed=�$/��)Добавить пользователя��$��30��$��5 15 5 5q�#��[ #��[ 