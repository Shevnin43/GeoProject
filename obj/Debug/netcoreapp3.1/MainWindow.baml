   M S B A M L   `   `   `  ����E  AGeoProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GeoProject
GeoProject  5   	       GeoProject.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GeoProject     5   	   -   5        $
��Main��5   	     � Title$8  2Шевнин С.А. Тестовая работа ��5	   	   $	��450��6.   $	��800��6;   .��5
      � .+�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   .+�5      ��  � ToolTip$! Введите запрос��6   $	��400��63   $��5,5,0,5q�6?   $��25��6�   ��6P   ��  �� Path$ 
SearchText� �� ElementName$
 Main�� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      -   5        $��
Найти��5      $��5q�6)   $��25��6K   �� 5      -   5         ��Name$ 
ScalePanel��5   6   $ 
Horizontal=�6   $y�	Collapsed=�6J   .+�5      �� $ Vertical=�6   $��5q�64   .+�5      �� -   5        	 ��Name$	 Delay��5   *   
 � Minimum$
 1Q�6    � Maximum$ 10Q�69    �� Orientation$ 
Horizontal=�6F   $	��100��6_    �� TickPlacement$ BottomRight=�6k    �� IsSnapToTickEnabled$
 True��6�    �� TickFrequency$ 1Q�6�    6�   �� $ Delay��$ Value�6�   �� 5      $ 
Horizontal=�6!   .+�5      �� $(��"Пропустить каждую ��6$   �� 5      ��6$   �� $ Delay��$ Value��� 5      $�� точку��6$   5      5      �� 5      -   5        $��Обновить��5      $��5q�6,   $��25��69   6E   �� 5      -   5        $��Сохранить ...��5      $��25��65   $��	10,5,10,5q�6A   6T   5      5      �� 5       -   5         ��Name$ 
DrawCanvas��5    "   $��1��6   $
��Auto��66   5"      5#      5$      5%      