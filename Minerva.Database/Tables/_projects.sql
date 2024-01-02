CREATE TABLE `minerva`.`_projects` (
  `id_Projects` INT NOT NULL AUTO_INCREMENT,
  `Filename` VARCHAR(50) DEFAULT (UUID()) NOT NULL,
  `Loanamount` DECIMAL(8,0) NULL,
  `Assignrdstaff` VARCHAR(50) NULL,
  `Filedescription` VARCHAR(5000) NULL,
  `staffnote` VARCHAR(5000) NULL,
  `primaryborrower` VARCHAR(50) NULL,
  `Primarybusiness` VARCHAR(45) NULL,
  `startdate` DATE NULL,
  `desiredclosingdate` DATE NULL,
  `initialphase` VARCHAR(45) NULL,
  `CreateDateTime` DATETIME DEFAULT CURRENT_TIMESTAMP,
  INDEX `Assignrdstaff_FK_idx` (`Assignrdstaff` ASC) VISIBLE,
  INDEX `primaryborrower_FK_idx` (`primaryborrower` ASC) VISIBLE,
  PRIMARY KEY (`id_Projects`),
  CONSTRAINT `Assignrdstaff_FK`
    FOREIGN KEY (`Assignrdstaff`)
    REFERENCES `minerva`.`_users` (`userId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `primaryborrower_FK`
    FOREIGN KEY (`primaryborrower`)
    REFERENCES `minerva`.`_users` (`userId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);
