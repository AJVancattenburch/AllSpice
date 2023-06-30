CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255) NOT NULL COMMENT 'Recipe Name',
  instructions TEXT NOT NULL COMMENT 'Recipe Instructions',
  img VARCHAR(500) COMMENT 'Recipe Picture',
  category VARCHAR(255) NOT NULL COMMENT 'Recipe Category',
  archived BOOLEAN DEFAULT false COMMENT 'Recipe Archived',
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

/* DROP TABLE recipes; */

CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL COMMENT 'Ingredient Name',
  quantity VARCHAR(255) NOT NULL COMMENT 'Ingredient Quantity',
  recipeId INT NOT NULL COMMENT 'Recipe Id',
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

/* DROP TABLE ingredients; */

CREATE TABLE IF NOT EXISTS favorites(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  recipeId INT NOT NULL COMMENT 'Recipe Id',
  accountId VARCHAR(255) NOT NULL COMMENT 'Account Id',
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO recipes
  (title, instructions, img, category, archived, creatorId)
VALUES
('Best Oven-Baked Chicken', 'Preheat oven to 425°F/220°C (200°C fan).
Pound chicken to 1.5cm / 0.6″ at the thickest part; using a rolling pin, meat mallet or even your fist (key tip for even cooking + tender chicken). Mix Seasoning. Line tray with foil and baking / parchment paper. Place chicken upside down on tray. Drizzle chicken with about 1 tsp oil. Rub over with fingers. Sprinkle with Seasoning. Flip chicken. Drizzle with 1 tsp oil, rub with fingers, sprinkle with Seasoning, covering as much of the surface area as you can. Bake 18 minutes, or until surface is golden per photos and video, or internal temperature is 165°F/75°C using a meat thermometer. Remove from oven and immediately transfer chicken to serving plates. Wait 3 to 5 minutes before serving, garnished with freshly chopped parsley if desired. Pictured with a side of Garlic Butter Rice with Kale.', 'https://i.ibb.co/8PKj97g/oven-Baked-Chicken.jpg', 'American', false, '647fe77f07eaa2e6662ac239');

DELETE FROM recipes WHERE id = LAST_INSERT_ID();

DELETE FROM accounts WHERE id = '647fe77f07eaa2e6662ac239';


SELECT 
* 
FROM recipes 
ORDER BY `createdAt` DESC;

SELECT
*
FROM recipes
JOIN accounts ON recipes.creatorId = accounts.id
WHERE accounts.id = '647fe77f07eaa2e6662ac239';


SELECT
  rec.*,
  creator.*
FROM recipes rec
JOIN accounts creator
  ON rec.creatorId = creator.id
WHERE creator.id = '647fe77f07eaa2e6662ac239';

SELECT
  rec.*,
  creator.*
FROM recipes rec
JOIN accounts creator ON rec.creatorId = creator.id
WHERE rec.id = '647fe77f07eaa2e6662ac239';

SELECT
  title,
  name
FROM recipes
JOIN accounts
  ON recipes.creatorId = accounts.id
WHERE accounts.id = '647fe77f07eaa2e6662ac239';

SELECT
*
FROM recipes rec
JOIN ingredients ing
  ON rec.id = ing.recipeId
JOIN accounts creator 
  ON rec.creatorId = creator.id;
WHERE rec.id = '647fe77f07eaa2e6662ac239';


-- SECTION FAVORITES

INSERT INTO favorites
  (accountId, recipeId)
VALUES
  ('647fe77f07eaa2e6662ac239', LAST_INSERT_ID());

SELECT
account
FROM favorites favs
JOIN accounts account
  ON account.id = fav.accountId
WHERE fav.recipeId = 1;