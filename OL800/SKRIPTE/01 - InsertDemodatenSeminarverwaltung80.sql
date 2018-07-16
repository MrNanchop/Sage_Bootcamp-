/*

	Developer Bootcamp - Beispiel
	Anlageskript Demodaten
	Datum:		09.07.2018

*/

DELETE FROM PSDSeminartermine
GO

/* KHKAdressen */
INSERT [dbo].[KHKAdressen] ([Adresse], [Mandant], [Kategorie], [Matchcode], [Anrede], [Name1], [Name2], [LieferZusatz], [LieferStrasse], [LieferLand], [LieferPLZ], [LieferOrt], [PostZusatz], [PostStrasse], 
[PostLand], [PostPLZ], [PostOrt], [Ansprache], [Telefon], [Telefax], [Mobilfunk], [EMail], [Homepage], [Memo], [Sprache], [Erstkontakt], [Gruppe], [A1Besteuerung], [Auswertungskennzeichen], [Referenz], 
[Aktiv], [Abladestelle], [Werkschluessel], [USER_Absagetage], [USER_Schulungsstandort]) VALUES (78, 123, 0, N'Schulungszentrum Frankfurt, Frankfurt', N'Firma                                             ', N'Schulungszentrum Frankfurt', NULL, NULL, N'Emil-von-Behring-Str. 8-14', N'DE', N'60439', N'Frankfurt', NULL, NULL, NULL, NULL, NULL, N'Sehr geehrte Damen und Herren', N'069-50007-0', NULL, NULL, N'Info@SchulungszentrumFFM.de', N'www.Schulungszentrum.de', NULL, N'D', CAST(0x0000A32700000000 AS DateTime), N'FA', 0, NULL, NULL, -1, NULL, NULL, 10, -1)
GO
INSERT [dbo].[KHKAdressen] ([Adresse], [Mandant], [Kategorie], [Matchcode], [Anrede], [Name1], [Name2], [LieferZusatz], [LieferStrasse], [LieferLand], [LieferPLZ], [LieferOrt], [PostZusatz], [PostStrasse], 
[PostLand], [PostPLZ], [PostOrt], [Ansprache], [Telefon], [Telefax], [Mobilfunk], [EMail], [Homepage], [Memo], [Sprache], [Erstkontakt], [Gruppe], [A1Besteuerung], [Auswertungskennzeichen], [Referenz], 
[Aktiv], [Abladestelle], [Werkschluessel], [USER_Absagetage], [USER_Schulungsstandort]) VALUES (79, 123, 0, N'Schulungszentrum München, München', N'Firma                                             ', N'Schulungszentrum München', NULL, NULL, N'Albert-Schweitzer-Ring 19', N'DE', N'81735', N'München', NULL, NULL, NULL, NULL, NULL, N'Sehr geehrte Damen und Herren', N'089-50007-0', NULL, NULL, N'Info@SchulungszentrumFFM.de', N'www.Schulungszentrum.de', NULL, N'D', CAST(0x0000A32700000000 AS DateTime), N'FA', 0, NULL, NULL, -1, NULL, NULL, 10, -1)
GO
INSERT [dbo].[KHKAdressen] ([Adresse], [Mandant], [Kategorie], [Matchcode], [Anrede], [Name1], [Name2], [LieferZusatz], [LieferStrasse], [LieferLand], [LieferPLZ], [LieferOrt], [PostZusatz], [PostStrasse], 
[PostLand], [PostPLZ], [PostOrt], [Ansprache], [Telefon], [Telefax], [Mobilfunk], [EMail], [Homepage], [Memo], [Sprache], [Erstkontakt], [Gruppe], [A1Besteuerung], [Auswertungskennzeichen], [Referenz], 
[Aktiv], [Abladestelle], [Werkschluessel], [USER_Absagetage], [USER_Schulungsstandort]) VALUES (80, 123, 0, N'Schulungszentrum Hamburg, Hamburg', N'Firma                                             ', N'Schulungszentrum Hamburg', NULL, NULL, N'Amsinckstr. 70', N'DE', N'20097', N'Hamburg', NULL, NULL, NULL, NULL, NULL, N'Sehr geehrte Damen und Herren', N'040-50007-0', NULL, NULL, N'Info@SchulungszentrumHH.de', N'www.Schulungszentrum.de', NULL, N'D', CAST(0x0000A32700000000 AS DateTime), N'FA', 0, NULL, NULL, -1, NULL, NULL, 5, -1)
GO

/* KHKTan */
UPDATE KHKTan SET [Tan] = (SELECT MAX([Tan]) FROM KHKTan WHERE Tabelle = 'KHKAdressen' AND Mandant = 123) WHERE Tabelle = 'KHKAdressen' AND Mandant = 123
GO

/* KHKGruppen */

INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500001, N'Krankheit', 123, N'Trainer ist krank geworden', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500001, N'Unwetter', 123, N'Trainer ist das Wetter zu schlecht', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500001, N'Fussball', 123, N'Trainer ist beim Länderspiel', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500002, N'Neu', 123, N'Neu', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500002, N'Terminiert', 123, N'Termin ist beim Schulungszentrum gefixt', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500002, N'Beworben', 123, N'Marketingmaßnahmen sind eingeleitet', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (1000500002, N'Beendet', 123, N'Erfolgreich - naja, ok, vielleicht - beendet', NULL, NULL)
GO
INSERT [dbo].[KHKGruppen] ([Typ], [Gruppe], [Mandant], [Bezeichnung], [Tag], [Memo]) VALUES (40046, N'Consultant', 123, N'Consultant', NULL, NULL)
GO

/* KHKMitarbeiter */

INSERT [dbo].[KHKMitarbeiter] ([Nummer], [Mandant], [Matchcode], [Gruppe], [UserName], [Verrechnungssatz], [ExterneNummer], [ExternerMandant], [Memo], [Aktiv], [Kartennummer], [Schichtplannummer], 
[Pausenabzug], [Kostentraeger], [Kostenstelle], [USER_Trainer]) VALUES (N'M10001', 123, N'Thomas Ostermann', N'Consultant', NULL, 0.0000, 0, 0, NULL, -1, NULL, NULL, 0, NULL, NULL, -1)
GO
INSERT [dbo].[KHKMitarbeiter] ([Nummer], [Mandant], [Matchcode], [Gruppe], [UserName], [Verrechnungssatz], [ExterneNummer], [ExternerMandant], [Memo], [Aktiv], [Kartennummer], [Schichtplannummer], 
[Pausenabzug], [Kostentraeger], [Kostenstelle], [USER_Trainer]) VALUES (N'M10002', 123, N'Thomas Fritz', N'Consultant', NULL, 0.0000, 0, 0, NULL, -1, NULL, NULL, 0, NULL, NULL, -1)
GO
INSERT [dbo].[KHKMitarbeiter] ([Nummer], [Mandant], [Matchcode], [Gruppe], [UserName], [Verrechnungssatz], [ExterneNummer], [ExternerMandant], [Memo], [Aktiv], [Kartennummer], [Schichtplannummer], 
[Pausenabzug], [Kostentraeger], [Kostenstelle], [USER_Trainer]) VALUES (N'M10003', 123, N'Christian Weigel', N'Consultant', NULL, 0.0000, 0, 0, NULL, -1, NULL, NULL, 0, NULL, NULL, -1)
GO

/* KHKArtikel */

INSERT [dbo].[KHKArtikel] ([Artikelnummer], [Mandant], [Bezeichnung1], [Bezeichnung2], [Matchcode], [Langtext], [Dimensionstext], [DimensionstextHTML], [DimensionstextRTF], [Memo], [Artikelgruppe], [Ersatzartikelnummer], [Hersteller], [HArtikelnummer], 
[Steuerklasse], [Erloescode], [Wareneingangscode], [Wareneinsatzcode], [Warenbestandcode], [SachkontoVK], [SachkontoEK], [SachkontoWZ], [SachkontoWB], [IstEinmalartikel], [IstProvisionierbar], [Nachweispflicht], [Verkaufsmengeneinheit], 
[MengenberechnungVK], [DezimalstellenVK], [UmrechnungsfaktorVK], [VerpackungseinheitVK], [UmrechnungsfaktorVPVK], [PreiseinheitVK], [Basismengeneinheit], [DezimalstellenBasis], [Lagermengeneinheit], [DezimalstellenLager], [UmrechnungsFaktorLME], 
[Lagerfuehrung], [Lagerplatzzuordnung], [Gewichtseinheit], [PreispflegeEK], [IstBestellartikel], [IstVerkaufsartikel], [Stuecklistentyp], [IstKundenkartei], [IstLieferantenkartei], [IstRabattfaehig], [Gemeinkostenzuschlag], [Bezugskostenzuschlag], 
[Gewinnzuschlag], [Bewertungssatz], [Kostenstelle], [Kostentraeger], [IstFavorit], [IstVorlage], [Chargenpflicht], [Warennummer], [Ursprungsregion], [Bestimmungsregion], [Rabattgruppe], [Laenge], [Breite], [Hoehe], [IntrastatMengeneinheit], 
[UmrechnungsfaktorIntraME], [DezimalstellenIntraME], [Hauptartikelgruppe], [Vaterartikelgruppe], [Grundpreiseinheit], [UmrechnungsfaktorGP], [GrundpreisBasis], [PlatzID], [Entnahmeverfahren], [Variante], [Inventurmethode], [Aktiv], [Artikelart], 
[RessourcenRef], [Zeichnungsnummer], [GemeinkostenID], [MinimaleLosgroesse], [MaximaleLosgroesse], [IstFertigungsartikel], [IstUnterbaugruppe], [IstErsatzteil], [IstVerschleissteil], [Schwundart], [Schwund], [PKonfigIstKonfigArtikel], [PKonfigAnwendung], 
[IstFremdleistung], [KalkulationsLosgroesse], [Ursprungsland], [StatWertEingang], [StatWertVersendung], [Eigenmasse], [StatVerfahren], [DezimalstellenPreis], [Verbrauchsteuer], [Besteuerungsart], [LangtextHTML], [LangtextRTF], [USER_Seminar], 
[USER_AnzahlTage], [USER_UhrzeitVon], [USER_UhrzeitBis]) VALUES (N'55550001', 123, N'Sage 100 Developer Bootcamp', NULL, N'Sage 100 Developer Bootcamp', NULL, NULL, NULL, NULL, NULL,
 N'060', NULL, NULL, NULL, 1, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, -1, 0, N'Stk', NULL, 0, 1.0000, NULL, 1.0000, 1, N'Stk', 0, N'Stk', 0, 1.0000, 0, 0, NULL, 0, 0, -1, 0, -1, -1, -1, 0.0000, 0.0000, 0.0000, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL
 , NULL, 0.0000, 0.0000, 0.0000, NULL, 1.0000, 0, N'060', N'EMPTY', NULL, 1.0000, 1.0000, 0, NULL, NULL, 0, -1, 0, NULL, NULL, NULL, 0.0000, 0.0000, 0, -1, 0, 0, N'M', 0.0000, 0, NULL, 0, 0.0000, NULL, NULL, NULL, NULL, NULL, 2, 0, -1, NULL, NULL, -1, 5,
  N'09:30', N'17:30')
GO
INSERT [dbo].[KHKArtikel] ([Artikelnummer], [Mandant], [Bezeichnung1], [Bezeichnung2], [Matchcode], [Langtext], [Dimensionstext], [DimensionstextHTML], [DimensionstextRTF], [Memo], [Artikelgruppe], [Ersatzartikelnummer], [Hersteller], [HArtikelnummer], 
[Steuerklasse], [Erloescode], [Wareneingangscode], [Wareneinsatzcode], [Warenbestandcode], [SachkontoVK], [SachkontoEK], [SachkontoWZ], [SachkontoWB], [IstEinmalartikel], [IstProvisionierbar], [Nachweispflicht], [Verkaufsmengeneinheit], 
[MengenberechnungVK], [DezimalstellenVK], [UmrechnungsfaktorVK], [VerpackungseinheitVK], [UmrechnungsfaktorVPVK], [PreiseinheitVK], [Basismengeneinheit], [DezimalstellenBasis], [Lagermengeneinheit], [DezimalstellenLager], [UmrechnungsFaktorLME], 
[Lagerfuehrung], [Lagerplatzzuordnung], [Gewichtseinheit], [PreispflegeEK], [IstBestellartikel], [IstVerkaufsartikel], [Stuecklistentyp], [IstKundenkartei], [IstLieferantenkartei], [IstRabattfaehig], [Gemeinkostenzuschlag], [Bezugskostenzuschlag], 
[Gewinnzuschlag], [Bewertungssatz], [Kostenstelle], [Kostentraeger], [IstFavorit], [IstVorlage], [Chargenpflicht], [Warennummer], [Ursprungsregion], [Bestimmungsregion], [Rabattgruppe], [Laenge], [Breite], [Hoehe], [IntrastatMengeneinheit], 
[UmrechnungsfaktorIntraME], [DezimalstellenIntraME], [Hauptartikelgruppe], [Vaterartikelgruppe], [Grundpreiseinheit], [UmrechnungsfaktorGP], [GrundpreisBasis], [PlatzID], [Entnahmeverfahren], [Variante], [Inventurmethode], [Aktiv], [Artikelart], 
[RessourcenRef], [Zeichnungsnummer], [GemeinkostenID], [MinimaleLosgroesse], [MaximaleLosgroesse], [IstFertigungsartikel], [IstUnterbaugruppe], [IstErsatzteil], [IstVerschleissteil], [Schwundart], [Schwund], [PKonfigIstKonfigArtikel], [PKonfigAnwendung], 
[IstFremdleistung], [KalkulationsLosgroesse], [Ursprungsland], [StatWertEingang], [StatWertVersendung], [Eigenmasse], [StatVerfahren], [DezimalstellenPreis], [Verbrauchsteuer], [Besteuerungsart], [LangtextHTML], [LangtextRTF], [USER_Seminar], 
[USER_AnzahlTage], [USER_UhrzeitVon], [USER_UhrzeitBis]) VALUES (N'55550002', 123, N'Sage 100 Systemausbildung', NULL, N'Sage 100 Systemausbildung', NULL, NULL, NULL, NULL, NULL, N'060', 
NULL, NULL, NULL, 1, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, -1, 0, N'Stk', NULL, 0, 1.0000, NULL, 1.0000, 1, N'Stk', 0, N'Stk', 0, 1.0000, 0, 0, NULL, 0, 0, -1, 0, -1, -1, -1, 0.0000, 0.0000, 0.0000, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, NULL,
 0.0000, 0.0000, 0.0000, NULL, 1.0000, 0, N'060', N'EMPTY', NULL, 1.0000, 1.0000, 0, NULL, NULL, 0, -1, 0, NULL, NULL, NULL, 0.0000, 0.0000, 0, -1, 0, 0, N'M', 0.0000, 0, NULL, 0, 0.0000, NULL, NULL, NULL, NULL, NULL, 2, 0, -1, NULL, NULL, -1, 2,
  N'09:30', N'17:30')
GO
INSERT [dbo].[KHKArtikel] ([Artikelnummer], [Mandant], [Bezeichnung1], [Bezeichnung2], [Matchcode], [Langtext], [Dimensionstext], [DimensionstextHTML], [DimensionstextRTF], [Memo], [Artikelgruppe], [Ersatzartikelnummer], [Hersteller], [HArtikelnummer], 
[Steuerklasse], [Erloescode], [Wareneingangscode], [Wareneinsatzcode], [Warenbestandcode], [SachkontoVK], [SachkontoEK], [SachkontoWZ], [SachkontoWB], [IstEinmalartikel], [IstProvisionierbar], [Nachweispflicht], [Verkaufsmengeneinheit], 
[MengenberechnungVK], [DezimalstellenVK], [UmrechnungsfaktorVK], [VerpackungseinheitVK], [UmrechnungsfaktorVPVK], [PreiseinheitVK], [Basismengeneinheit], [DezimalstellenBasis], [Lagermengeneinheit], [DezimalstellenLager], [UmrechnungsFaktorLME], 
[Lagerfuehrung], [Lagerplatzzuordnung], [Gewichtseinheit], [PreispflegeEK], [IstBestellartikel], [IstVerkaufsartikel], [Stuecklistentyp], [IstKundenkartei], [IstLieferantenkartei], [IstRabattfaehig], [Gemeinkostenzuschlag], [Bezugskostenzuschlag], 
[Gewinnzuschlag], [Bewertungssatz], [Kostenstelle], [Kostentraeger], [IstFavorit], [IstVorlage], [Chargenpflicht], [Warennummer], [Ursprungsregion], [Bestimmungsregion], [Rabattgruppe], [Laenge], [Breite], [Hoehe], [IntrastatMengeneinheit], 
[UmrechnungsfaktorIntraME], [DezimalstellenIntraME], [Hauptartikelgruppe], [Vaterartikelgruppe], [Grundpreiseinheit], [UmrechnungsfaktorGP], [GrundpreisBasis], [PlatzID], [Entnahmeverfahren], [Variante], [Inventurmethode], [Aktiv], [Artikelart], 
[RessourcenRef], [Zeichnungsnummer], [GemeinkostenID], [MinimaleLosgroesse], [MaximaleLosgroesse], [IstFertigungsartikel], [IstUnterbaugruppe], [IstErsatzteil], [IstVerschleissteil], [Schwundart], [Schwund], [PKonfigIstKonfigArtikel], [PKonfigAnwendung], 
[IstFremdleistung], [KalkulationsLosgroesse], [Ursprungsland], [StatWertEingang], [StatWertVersendung], [Eigenmasse], [StatVerfahren], [DezimalstellenPreis], [Verbrauchsteuer], [Besteuerungsart], [LangtextHTML], [LangtextRTF], [USER_Seminar], 
[USER_AnzahlTage], [USER_UhrzeitVon], [USER_UhrzeitBis]) VALUES (N'55550003', 123, N'Sage 100 AppDesigner-Ausbildung', NULL, N'Sage 100 AppDesigner-Ausbildung', NULL, NULL, NULL, 
NULL, NULL, N'060', NULL, NULL, NULL, 1, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, -1, 0, N'Stk', NULL, 0, 1.0000, NULL, 1.0000, 1, N'Stk', 0, N'Stk', 0, 1.0000, 0, 0, NULL, 0, 0, -1, 0, -1, -1, -1, 0.0000, 0.0000, 0.0000, NULL, NULL, NULL, 0, 0, 0, NULL, 
NULL, NULL, NULL, 0.0000, 0.0000, 0.0000, NULL, 1.0000, 0, N'060', N'EMPTY', NULL, 1.0000, 1.0000, 0, NULL, NULL, 0, -1, 0, NULL, NULL, NULL, 0.0000, 0.0000, 0, -1, 0, 0, N'M', 0.0000, 0, NULL, 0, 0.0000, NULL, NULL, NULL, NULL, NULL, 2, 0, -1, NULL, 
NULL, -1, 1, N'09:30', N'17:30')
GO

/* KHKPreislistenArtikel */

INSERT [dbo].[KHKPreislistenArtikel] ([Mandant], [ListeID], [Artikelnummer], [AuspraegungID], [AbMenge], [Einzelpreis]) VALUES (123, 2, N'55550001', 0, 0.0000, 1940.0000)
GO
INSERT [dbo].[KHKPreislistenArtikel] ([Mandant], [ListeID], [Artikelnummer], [AuspraegungID], [AbMenge], [Einzelpreis]) VALUES (123, 3, N'55550001', 0, 0.0000, 1590.0000)
GO
INSERT [dbo].[KHKPreislistenArtikel] ([Mandant], [ListeID], [Artikelnummer], [AuspraegungID], [AbMenge], [Einzelpreis]) VALUES (123, 2, N'55550002', 0, 0.0000, 590.0000)
GO
INSERT [dbo].[KHKPreislistenArtikel] ([Mandant], [ListeID], [Artikelnummer], [AuspraegungID], [AbMenge], [Einzelpreis]) VALUES (123, 3, N'55550002', 0, 0.0000, 450.0000)
GO
INSERT [dbo].[KHKPreislistenArtikel] ([Mandant], [ListeID], [Artikelnummer], [AuspraegungID], [AbMenge], [Einzelpreis]) VALUES (123, 2, N'55550003', 0, 0.0000, 350.0000)
GO
INSERT [dbo].[KHKPreislistenArtikel] ([Mandant], [ListeID], [Artikelnummer], [AuspraegungID], [AbMenge], [Einzelpreis]) VALUES (123, 3, N'55550003', 0, 0.0000, 300.0000)
GO

/* PSDSeminartermine */
INSERT [dbo].[PSDSeminartermine] ([SeminarterminId], [Mandant], [Matchcode], [Artikelnummer], [TrainerIDEins], [TrainerIDZwei], [Startdatum], [Endedatum], [Startzeit], [Endezeit], [AnzahlTeilnehmer], [AnzahlTeilnehmerMax], 
[AnzahlTeilnehmerMin], [Stornofrist], [Adresse], [PLZ], [Ort], [Absagetermin], [Abgesagt], [Absagegrund], [Status], [Memo], [Aktiv]) VALUES (N'S100001', 123, N'Sage 100 Developer Bootcamp, Frankfurt', N'55550001', N'M10001', N'M10002', 
CONVERT(DateTime, N'22.11.2018', 104), CONVERT(DateTime, N'27.11.2018', 104), N'09:30', N'17:30', NULL, 12, 3, NULL, 78, '60439', 'Frankfurt', NULL, 0, NULL, N'Neu', NULL, -1)
GO

INSERT [dbo].[PSDSeminartermine] ([SeminarterminId], [Mandant], [Matchcode], [Artikelnummer], [TrainerIDEins], [TrainerIDZwei], [Startdatum], [Endedatum], [Startzeit], [Endezeit], [AnzahlTeilnehmer], [AnzahlTeilnehmerMax], 
[AnzahlTeilnehmerMin], [Stornofrist], [Adresse], [PLZ], [Ort], [Absagetermin], [Abgesagt], [Absagegrund], [Status], [Memo], [Aktiv]) VALUES (N'S100002', 123, N'Sage 100 Systemausbildung, München', N'55550002', N'M10002', NULL, 
CONVERT(DateTime, N'05.11.2018', 104), CONVERT(DateTime, N'06.11.2018', 104), N'09:30', N'17:30', NULL, 12, 3, NULL, 80, '80808', 'München', NULL, 0, NULL, N'Neu', NULL, -1)
GO

INSERT [dbo].[PSDSeminartermine] ([SeminarterminId], [Mandant], [Matchcode], [Artikelnummer], [TrainerIDEins], [TrainerIDZwei], [Startdatum], [Endedatum], [Startzeit], [Endezeit], [AnzahlTeilnehmer], [AnzahlTeilnehmerMax], 
[AnzahlTeilnehmerMin], [Stornofrist], [Adresse], [PLZ], [Ort], [Absagetermin], [Abgesagt], [Absagegrund], [Status], [Memo], [Aktiv]) VALUES (N'S100003', 123, N'Sage 100 AppDesigner-Ausbildung, Hamburg', N'55550003', N'M10002', NULL, 
CONVERT(DateTime, N'21.11.2018', 104), CONVERT(DateTime, N'22.11.2018', 104), N'09:30', N'17:30', NULL, 12, 3, NULL, 80, '20101', 'Hamburg', NULL, 0, NULL, N'Beworben', NULL, -1)
GO

