   M S B A M L   `   `   `  ����I  E3DTools, Version=1.0.6702.30055, Culture=neutral, PublicKeyToken=null4'clr-namespace:_3DTools;assembly=3DTools_3DTools  5       	sw_en_GUI   sw_en_GUI.Window1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;Tools3D'clr-namespace:_3DTools;assembly=3DTools     5      -   5          �Name$  WindowFor3D��5   0    � Title$ 
3D drawing��6    � WindowState$ 	Maximized=�6   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��30px �6   6,   �� 5	      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��80px �6   6.   �� 5      $��* �6   6+   .+�5   
   !   _3DTools.TrackballDecorator  -   5          Name$ myTrackDecorator��5   B   $��1��6%   $��1��65   "   _3DTools.Viewport3DDecorator   Content. 5      '� -   5        $��myViewport3D��5       '� Camera 5      =� 5      -   5        myCamera�� � LookDirection$ 0,0,-15�5   :   	 � Position$	 0,0,10001�6Q   
 � NearPlaneDistance$
 1Q�5       � FarPlaneDistance$ 100000Q�64   .x�5      g�  g� Content 5      h� 5      .��5      ��  �� Color$ #AA0000��6+   t� 5      $ White��6/    t� 	Direction$ 0,0,-15�6=   5      5      5      5      �� 5   
   $��0��6   $��0��6!   
�������5       �� 5!   
   $��0��6   $��1��6%   
�������.+�5"      �� -   5         ��Name$ ButtonTest4��5"      $��25��6*   $��75��66   .��6[   Test46b   �� 5#      -   5        $ ButtonTest5��5#      $��25��6*   $��75��66   .��6[   Test56b   �� 5$      -   5         ��Name$ 
SliderStep��5$       � Minimum$ 1Q�6)    � Maximum$ 20Q�65    � Value$ 1Q�6B   $
��80px��6L   |�.   � ToolTip 5%      ��  �� Self �� RelativeSource# Z  �� Path$ 
(int)Value�6i   �� 5&      .��6   Step6   �� 5'      -   5        $ ButtonClear��5'      $��25��6*   $��75��66   .��6[   Clear6b   5)      �� 5*   
   $��1��6   $��0��6!   
�������6H   5,      5/      