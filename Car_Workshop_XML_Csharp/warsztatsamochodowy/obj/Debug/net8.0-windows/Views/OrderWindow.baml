   M S B A M L   `   `   `  ����N  Jwarsztatsamochodowy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:warsztatsamochodowy.Viewswarsztatsamochodowy.Views  5   	   +    %warsztatsamochodowy.Views.OrderWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:warsztatsamochodowy.Views     5   	     � Title$  OrderWindow��5   	   $	��450��6   $	��800��6*   .��5	      � .+�5
   
   �� $��Creation Date:��6   $	��Top=�6*   $��	10,10,0,0q�6B   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ CreationDate��5      $	��Top=�6+   $��120,10,10,0q�6C      Text 6X   �� *CreationDate+�� 5   
   $��Status:��6   $	��Top=�6#   $��10,180,743,0q�6;   �� 5   
   -   5         ��Name$ StatusComboBox��5      $
��Left=�5      $	��Top=�5      $��120,180,0,0q�5      $	��300��5       �� SelectedValuePath$ Content��5       �� SelectedValue 5      �� *Status+ �� Mode$ TwoWay=�.@�5      �� $��Pending��6   �� 5      $��
InProgress��6   �� 5      $��	Completed��6   5      �� 5   
   $��Description:��6   $	��Top=�6(   $��	10,70,0,0q�6@   �� 5   
   -   5         ��Name$ Description��5      $	��Top=�6'   $��120,70,10,0q�6?   $��60��6T   ��6`   �� *Description+�� 5   
   $
��Car:��6   $	��Top=�6    $��
10,150,0,0q�68   �� 5   
   -   5        $ CarComboBox��5       �� DisplayMemberPath$ Brand��5      $	��Top=�5       $��120,150,380,0q�6+   $ Id��5"       5!      �� *SelectedCarId+��5#      �� *Cars+�� 5%   
   $��Client:��6   $	��Top=�6#   $��
10,217,0,0q�6;   �� 5&   
   -   5        $ ClientComboBox��5&      $ 	FirstName��5'      $	��Top=�5(      $��120,214,380,0q�6+   $ Id��5*       5)      �� *SelectedClientId+��5+      �� *	Clients+�� 5-   
   $��	Add Order��6   $��Right=�6%   $��Bottom=�6A   $��
10,0,10,10q�6\   $	��100��5.   5   ��6   �� *AddOrderCommand+65   5/      50      