   M S B A M L   `   `   `  ����G  CDiplomClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DiplomClientDiplomClient  5           DiplomClient.PatientWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ,localclr-namespace:DiplomClient     5        � Title$  PatientWindow��5      $	��744��5	      $	��850��5
       � SizeToContent$ WidthAndHeight=�5      .��5      � ��5      �� 5       �� ImageSource$ background_img_mirror.png��6    j� Stretch$ Uniform=�5       � ColumnDefinitions 5      �� 5      -   5         ��Name$
 Col0��5      $
��Auto �5   
   �� 5      -   5        $
 Col1��5      $
��Auto �5   
   �� 5      -   5        $
 Col2��5      $
��Auto �5   
    � RowDefinitions 5      �� 5      -   5         ��Name$
 Row0��5      $	��160 �5   
   6   �� 5      -   5        $
 Row1��5      $
��Auto �5   
   6   �� 5      -   5        $
 Row2��5      $
��Auto �5    
   6   .+�5"      �� -   5         ��Name$ VisitBookingButton��5"      $��0��5#      $��3��5$      $,��&Записатись на прийом��5%      $��Center=�5&      $��Center=�5'      $��20,-120,20,0q�5(      $	��130��5)      $��20��5+      �� 5,      -   5        $ MyVisitsButton��5,      $��0��5-      $��3��5.      $��Мої візити��5/      $��Center=�50      $��Center=�51      $��20,-60,20,0q�52      $	��130��53      $��20��55      �� 56      -	   5        $ BillsButton��56      $��0��57      $��3��58      $��Мої рахунки��59      $��Center=�5:      $��Center=�5;      $��	20,0,20,0q�5<      $	��130��5=      $��20��5?      �� 5@      -
   5        $ LKButton��5@      $��0��5A      $��3��5B      $��Подивитись ЛК��5C      $��Center=�5D      $��Center=�5E      $��
20,60,20,0q�5F      $	��130��5G      $��20��5I      �� 5J      -   5        $ 
ExitButton��5J      $��0��5K      $��3��5L      $��
Вихід��5M      $��Center=�5N      $��Center=�5O      $��20,180,20,0q�5P      $	��130��5Q      $��20��5R      5S      �� 5T      -   5        $ 
BackButton��5T      $��3��5U      $��0��5V      $��
Назад��5W      $��Center=�5X      $��Center=�5Y      $��20,120,20,0q�5Z      $��37��5[      $y�Hidden=�5]      $��20��5^      �� 5_      -   5        $ BookingButton��5_      $��3��5`      $��0��5a      $��Записатися��5b      $��Center=�5c      $��Center=�5d      $��20,-60,20,0q�5e      $��66��5f      $y�Hidden=�5h      $��20��5i      �� 5j      -   5        $ DeleteVisitButton��5j      $��3��5k      $��0��5l      $#��Відмінити візит��5m      $��Center=�5n      $��Center=�5o      $��	20,0,20,0q�5p      $
��Auto��5q      $y�Hidden=�5r      $��20��5s      5t      �� 5u      -   5        $ PayBillButton��5u      $��3��5v      $��0��5w      $%��Оплатити рахунок��5x      $��Center=�5y      $��Center=�5z      $��	20,0,20,0q�5{      $	��104��5|      $y�Hidden=�5~      $��20��5      �� 5�      -   5        	 ��Name$	 	MainLable��5�   
   $��2��5�      $��0��5�      $��Center=�5�      $	��Top=�5�      $i��cОберіть лікаря, розклад якого ви хотіли б переглянути:��5�      $
��Auto��5�      $y�Hidden=�5�      $
��Auto��5�      �� 5�      -   5        $	 PIBLable��5�   
   $��2��5�      $��1��5�      $	��PIB��5�      $��Center=�5�      $	��Top=�5�      $y�Hidden=�5�      $��25��5�      $
��Auto��5�      �� 5�      -   5        $	 ExperienceLable��5�   
   $��2��5�      $��1��5�      $
��Expa��5�      $��Center=�5�      $��
0,25,323,0q�5�      $	��Top=�5�      $y�Hidden=�5�      $��25��5�      $
��Auto��5�      �� 5�      -   5        $	 DegreeLable��5�   
   $��2��5�      $��1��5�      $��Degree��5�      $��Center=�5�      $��
326,25,0,0q�5�      $	��Top=�5�      $y�Hidden=�5�      $��25��5�      $
��Auto��5�      �� 5�      -   5        $	 ScheduleLable��5�   
   $��2��5�      $��1��5�      $��Розклад��5�      $��Center=�5�      $��0,50,0,0q�5�      $	��Top=�5�      $y�Hidden=�5�      $��25��5�      $
��Auto��5�      �� 5�      -   5        $	 CommentsLable��5�   
   $��2��5�      $��2��5�      $��Коментарі��5�      $��Center=�5�      $	��Top=�5�      $y�Hidden=�5�      $��25��5�      $
��Auto��5�      �� 5�      -   5        
 ��Name$
 DocImage��5�   
   $��1��5�      $��0��5�      $	��Top=�5�      $	��100��5�      $	��100��5�      $y�Hidden=�5�      $��0,20,0,0q�5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ MyBillsDataGrid��5�      $��2��5�      $��0��5�         
IsReadOnly . $y�Hidden=�5�      $��Center=�5�      $	��371��5�      $	��Top=�5�      $	��629��5�      $��20,20,20,20q�5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата видачі��6   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   
   �� *Date+  5�      $ 
Назва��6    5�   
   �� *Name+  5�      $ Ціна��6    5�   
   �� *Cost+  5�      $ ПІБ Лікаря��6    5�   
   �� *PIB+  5�      $ Login��6      
Visibility$ Hidden=�5�   
    5�   
   �� *Login+5�   
   5�        5�      -   5        $ MyVisitsDataGrid��5�      $��2��5�      $��0��5�       . $y�Hidden=�5�      $��Center=�5�      $	��371��5�      $	��Top=�5�      $	��629��5�      $��20,20,20,20q�5�       5�        5�      $ Id��6   $ Hidden=�5�   
    5�   
   �� *Id+5�   
     5�      $ Дата прийому��6    5�   
   �� *Date+  5�      $ Час прийому��6    5�   
   �� *Time+  5�      $ ПІБ Лікаря��6    5�   
   �� *PIB+  5�      $ Статус��6    5�   
   �� *Status+   ElementStyle 5�      �� 5�       �� 
TargetType" ��6    �� Triggers 5�   	   P� 5�   
    P� Property � �� P� Value$ Активний��5�      . �5�      ��  �� Property � �� �� Value
 ����5�      5�      P� 5�   
    � ��$ Завершений��5�      . �5�      ��  � ��
 �����5�      5�      P� 5�   
    � ��$ Не з'явився��5      . �5     ��  � �� �	#FFFF73735     5     P� 5  
    � ��$ Відмінений��5     . �5     ��  � �� �	#FFA6A6A65     5     5
  	   5     5       5     -   5        $ DoctorsListDataGrid��5     $��2��5     $��0��5     $��Center=�5     $	��Top=�5      . $	��100��5     $	��420��5     $y�Hidden=�5     $��30,30,30,30q�5      5       5     $ Id��6   $ Hidden=�5  
    5  
   �� *Id+5  
     5     $ Ім'я��6    5  
   �� *Name+  5      $ Прізвище��6    5!  
   �� *	Surname+  5"     $ Кабінет��6    5#  
   �� *Room+  5$     $ Login��6   $ Hidden=�5&  
    5%  
   �� *Id+5&  
     5'     $ Login��6   $ Hidden=�5)  
    5(  
   �� *Login+5)  
     5*     $ 
Experience��6   $ Hidden=�5,  
    5+  
   �� *
Experience+5,  
     5-     $ Rating��6   $ Hidden=�5/  
    5.  
   �� *Rating+5/  
     50     $ DocFoto��6   $ Hidden=�52  
    51  
   �� *	DocFoto+52  
   54       55     -   5        $ ScheduleDataGrid��55     $��2��56     $��1��57      . $��Center=�59     $	��200��5:     $��	0,80,0,10q�5;     $	��Top=�5<     $	��420��5=     $y�Hidden=�5>      5?       5@     $ Дата��6    5A  
   �� *Date+  5B     $ Час��6    5C  
   �� *Time+  5D     $! Вільно/Зайнято��6    5E  
   �� *	FreeOrNot+ 5F     �� 5G     " ��6    5H  	   P� 5I  
    � ��$
 True��5J     . �5K     ��  � ��
 ����5L     5M     P� 5N  
    � ��$ False��5O     . �5P     ��  � �� �	#FFFF31315Q     5R     5T  	   5V       5W     $ TimeId��6   $ Hidden=�5Y  
    5X  
   �� *TimeId+5Y  
     5Z     $ Login��6   $ Hidden=�5\  
    5[  
   �� *Login+5\  
   5^     �� 5_     -   5         ��Name$ LKRichTextBox��5_     $��2��5`     $��1��5a     $��Center=�5b     $	��Top=�5c     $	��400��5d     $	��300��5e      �� 
IsReadOnly . $y�Hidden=�5g     $��20,20,20,20q�5h     �� 5i     -   5         ��Name$ CommentsScrollViewer��5i     $��2��5j     $��2��5k     $��Center=�5l     $	��Top=�5m     $	��150��5n     $	��420��5o     $��	0,30,0,30q�5p     $
��Auto=�5q     $��Visible=�5r     5t     5u     