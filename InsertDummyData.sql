
INSERT INTO `Author` VALUES (1,'J.R.R','Tolkien'),(2,'R.A.','Salvatore'),(3,'Geoorge R. R.','Martin'),(4,'Terry','Brooks'),(5,'Margaret','Weis'),(6,'David','Eddings'),(10,'Kieran','Gillen'),(11,'Salvador','Larroca'),(12,'Edgar','Delgado');
INSERT INTO `Book` VALUES (1,'Lord of the Rings','1988-03-03','Hardcover','978-0395489321'),(2,'Starlight Enclave','2021-08-05','Paperback','9780063085886'),(3,'Glacier\'s edge','2022-08-05','Hardcover','9780063029828'),(4,'The Redemption of Althalus','2000-07-03','Hardcover','978-0006514831'),(6,'Star Wars: Darth Vader','2021-10-10','Comicbook','9780785192558');
INSERT INTO `Category` VALUES (1,'Fantasy'),(2,'Scifi'),(3,'Horror'),(4,'Mystery'),(5,'Biography'),(6,'History'),(7,'Medical'),(8,'test2');
INSERT INTO `User` VALUES (1,'tester','pass','test@test.com','Testi','Testaaja','2022-10-06 12:55:00','2022-10-06 12:55:00'),(2,'tester2','pass2','test2@example.com','Testi2','Testaaja2','2022-10-06 12:55:00','2022-10-06 12:55:00'),(3,'test3','$2a$11$xPm3vH8JATwkf9rKSgT32OaJiL10/vfwaVAfY6MbENlj1gNvJvh9m','test3@example.com','Testi3','Testaaja3','2022-10-06 15:08:12','2022-10-06 15:08:12'),(4,'tester4','$2a$11$C5F31DTKACWvDcUYtP2Iou84aFf7vX/Y9U69Vf/nqGVJJzIFeuH.G','test@test.com','Bob','Smith','2022-10-07 13:02:32','2022-10-07 13:02:32');
INSERT INTO `AuthorBooks` VALUES (1,1),(2,2),(2,3),(6,4),(10,6),(11,6),(12,6);
INSERT INTO `BookCategories` VALUES (1,1),(2,1),(3,1);
INSERT INTO `UserBooks` VALUES (1,1),(1,2),(2,1),(2,2),(3,2),(4,1);
