   M S B A M L   `   `   `  ����B  >WpfGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfGameWpfGame  5   	       WpfGame.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfGame     5   	   -   5          �Name$  MWindow��5   	    � Title$ 
MainWindow��5	   	   $	��950��6   $	��800��6)    � 
ResizeMode$ NoResize=�6L    � 	Resources 5
         CommonStyle �� 
TargetType �� Property �� Value �� Color �� Opacity  PressedStyle  ButtonStyleMine	 �� ImageSource  ButtonStyleFlag��%?  &	        &	 �     &	 A    &	 *    �� 5   
   " ��6$   .�5      ��  � �� 5      �� 5      $
 Gray��6&   $	 0.8Q�63   5      �� 5       � ��$ 20(�6)   5      �� 5   
   " ��6%   .�5      ��  � �� 5      �� 5      $ White��6&   $ 1Q�64   5      �� 5       � ��$ 20(�6)   5      �� 5   
   " ��6(   .�5      ��  � �� 5      �� 5      $?	 9C:\Users\Raptor\Documents\WpfGame\WpfGame\Images\mine.png��6!   5       �� 5!       � ��$ 20(�6)   �� 5"       � |� .  6*   5#      �� 5$   
   " ��6(   .�5%      ��  � �� 5&      �� 5'      $C	 =C:\Users\Raptor\Documents\WpfGame\WpfGame\Images\download.png��6!   5)      �� 5/       � ��$ 20(�6)   �� 50       � |� .  6*   51      52      .��53      � $
��Left=�6   $	��951��6&   $	��Top=�63   $	��805��6K   $��0,0,-6,0q�6W   .+�54   
   A� -   5        
 A�Name$	
 map��54      $��-5,27,10,124q�6#    A� Rows$ 0��69    A� Columns$ 0��6B   $	��800��6N   $	��800��6Z   o� 55   
   $
��Left=�6   $��22��6*   $	��Top=�66   $	��800��6N   .@�56      m� -   5        $��Новая игра��56      6+   m� 57      -   5        $��Настройки��57      6*   58      59      5:      