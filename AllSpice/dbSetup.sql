CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture',
  bio VARCHAR(255) COMMENT 'User Bio'
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255) NOT NULL COMMENT 'Recipe Name',
  instructions TEXT NOT NULL COMMENT 'Recipe Instructions',
  img VARCHAR(500) COMMENT 'Recipe Picture',
  category VARCHAR(255) NOT NULL COMMENT 'Recipe Category',
  tags VARCHAR(255),
  popularity INT DEFAULT 0,
  archived BOOLEAN DEFAULT 0 COMMENT 'Recipe Archived',
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

ALTER TABLE recipes 
MODIFY COLUMN popularity INT DEFAULT 0;


CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL COMMENT 'Ingredient Name',
  quantity VARCHAR(255) NOT NULL COMMENT 'Ingredient Quantity',
  recipeId INT NOT NULL COMMENT 'Recipe Id',

  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';


CREATE TABLE IF NOT EXISTS favorites(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  recipeId INT NOT NULL COMMENT 'Recipe Id',
  accountId VARCHAR(255) NOT NULL COMMENT 'Account Id',

  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  UNIQUE (recipeId, accountId)
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT "Primary Key",
  title VARCHAR(255) NOT NULL COMMENT 'Comment Title',
  photo VARCHAR(500) COMMENT 'Comment Picture',
  body VARCHAR(1000) NOT NULL,
  tags VARCHAR(255),
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
  /* UNIQUE (recipeId, accountId) */
) default charset utf8mb4 COMMENT '';

INSERT INTO recipes
  (title, instructions, img, category, tags, popularity, archived, creatorId)
VALUES
('Best Oven-Baked Chicken', 'Preheat oven to 425°F/220°C (200°C fan).
Pound chicken to 1.5cm / 0.6″ at the thickest part; using a rolling pin, meat mallet or even your fist (key tip for even cooking + tender chicken). Mix Seasoning. Line tray with foil and baking / parchment paper. Place chicken upside down on tray. Drizzle chicken with about 1 tsp oil. Rub over with fingers. Sprinkle with Seasoning. Flip chicken. Drizzle with 1 tsp oil, rub with fingers, sprinkle with Seasoning, covering as much of the surface area as you can. Bake 18 minutes, or until surface is golden per photos and video, or internal temperature is 165°F/75°C using a meat thermometer. Remove from oven and immediately transfer chicken to serving plates. Wait 3 to 5 minutes before serving, garnished with freshly chopped parsley if desired. Pictured with a side of Garlic Butter Rice with Kale.', 'https://i.ibb.co/8PKj97g/oven-Baked-Chicken.jpg', 'American', 'chicken, oven, baked, best', 0, false, '647fe77f07eaa2e6662ac239'); 

DELETE FROM recipes WHERE id = LAST_INSERT_ID();

DELETE FROM accounts WHERE id = '647fe77f07eaa2e6662ac239';

INSERT INTO comments
  (title, photo, body, tags, accountId, recipeId)
VALUES
  ('Best Oven-Baked Chicken', 'https://i.ibb.co/8PKj97g/oven-Baked-Chicken.jpg', 'This is the best chicken I have ever had!', 'best, chicken, ever', '647fe77f07eaa2e6662ac239', 0);


-- RECIPE ORDER OF RECIPE CREATION
SELECT 
* 
FROM recipes 
ORDER BY recipes.createdAt DESC;

-- DROP LOGIC
DROP TABLE IF EXISTS comments,
favorites,
ingredients,
recipes,
accounts;

/* SELECT
*
FROM recipes
JOIN accounts ON recipes.creatorId = accounts.id
WHERE accounts.id = '647fe77f07eaa2e6662ac239'
ORDER BY recipes.createdAt DESC;


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
WHERE rec.id = '647fe77f07eaa2e6662ac239'; */


-- SECTION FAVORITES

/* INSERT INTO favorites
  (accountId, recipeId)
VALUES
  ('647fe77f07eaa2e6662ac239', LAST_INSERT_ID());

SELECT
account
FROM favorites favs
JOIN accounts account
  ON account.id = fav.accountId
WHERE fav.recipeId = 1; */