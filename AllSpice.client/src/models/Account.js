export class Account {
  constructor(data) {
    this.id = data.id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    // TODO add additional properties if needed
  }
}

// export class Profile {
//   constructor(data) {
//     this.id = data.id
//     this.email = data.email
//     this.name = data.name
//     this.picture = data.picture
//     this.userFavorites = data.userFavorites
//     this.userRecipes = data.userRecipes
//     // TODO add additional properties if needed
//   }
// }

// export class Account extends Profile {
//   constructor(data) {
//     super(data)
//     this.favoriteId = data.favoriteId
//     this.name = data.name
//   }
// }