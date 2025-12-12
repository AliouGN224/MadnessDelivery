<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:deliv="http://www.univ-grenoble-alpes.fr/l3miage/delivery">
    <xsl:output method="html"/>
    
    <xsl:template match="/">
        <html>
            <head>
                <title>Le classement des joueur par niveau</title>
            </head>
            <body>
                <h1>Classement des joueurs par niveau</h1>
                <xsl:call-template name="table-niveau">
                    <xsl:with-param name="difficulty" select="'FACILE'"/>
                </xsl:call-template>

                <xsl:call-template name="table-niveau">
                    <xsl:with-param name="difficulty" select="'MOYEN'"/>
                </xsl:call-template>

                <xsl:call-template name="table-niveau">
                    <xsl:with-param name="difficulty" select="'DIFFICILE'"/>
                </xsl:call-template>
            </body>
        </html>
    </xsl:template>
    
    <!-- Table d'un niveau -->
    <xsl:template name="table-niveau">
        <xsl:param name="difficulty"/>
          <h2>Niveau : <xsl:value-of select="$difficulty"/></h2>
        <table>
            <thead>
                <tr>
                    <th>Rang </th>
                    <th>Nom joueur</th>
                    <th>Prenom joueur</th>
                    <th>Meilleur</th>
                </tr>
            </thead>
            
            <tbody>
               <xsl:apply-templates select="deliv:joueurs/deliv:joueur[deliv:niveaux/deliv:niveau[deliv:difficulte = $difficulty]]">
                   <xsl:sort
                           data-type="number"
                           order="descending"
                           select="deliv:niveaux/deliv:niveau[deliv:difficulte = $difficulty]/deliv:scores/deliv:score/deliv:valeurScore[last()]"/>
               </xsl:apply-templates> 
            </tbody>
        </table>
    </xsl:template>
    
    <xsl:template match="deliv:joueur">
        <xsl:param name="difficulty"/>
        <tr>
            <td><xsl:value-of select="position()"/></td>
            <td><xsl:value-of select="deliv:nom"/></td>
            <td><xsl:value-of select="deliv:prenom"/></td>
            <td>
                <xsl:value-of select="
                deliv:niveaux/deliv:niveau[deliv:difficulte = $difficulty]
                  /deliv:scores/deliv:score
                  [not(deliv:valeurScore &lt; ../deliv:score/deliv:valeurScore)]
                  /deliv:valeurScore
              "/>
            </td>
        </tr>
    </xsl:template>
</xsl:stylesheet>  