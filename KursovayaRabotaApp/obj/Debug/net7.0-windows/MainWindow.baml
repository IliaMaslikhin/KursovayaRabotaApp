   M S B A M L   `   `   `  ����  KursovayaRabotaApp#    KursovayaRabotaApp.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$6  0Книжное издательство учет��5   	   $
��1600��6+   $
��1600��69   .��5      � -   5        $��MainGrid��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ buttonPanel��5      $��0��6*   $��2��67    �� Orientation$ 
Horizontal=�6K   $
��Left=�6d   .+�5      �� -   5        $��addAuthorButton��5      $#��Добавить автора��6,   �� 5      -   5        $��removeAuthorButton��5      $!��Удалить автора��6/   �� 5      -   5        $%��calculateTotalCirculationButton��5      $0��*Рассчитать общий тираж��6<   �� 5      -   5        $��findAuthorsButton��5      $��Найти авторов��6.   �� 5      -   5        $��addBookButton��5      $!��Добавить книгу��6*   �� 5      -   5        $��removeBookButton��5      $��Удалить книгу��6-   �� 5      -	   5        $��saveToFileButton��5      $$��Сохранить в файл��6-   �� 5      -
   5        $��loadFromFileButton��5      $(��"Загрузить из файла��6/   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��authorsDataGrid��5      $��1��6*   $��0��67      AutoGenerateColumns$ False��6G     5   
   -   5        $��booksDataGrid��5      $��1��6(   $��1��65   $ False��6E   $y�	Collapsed=�6a   5       5!      