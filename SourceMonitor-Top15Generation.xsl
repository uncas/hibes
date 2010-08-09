<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <!--- SourceMonitor Summary Xml File Generation created by Eden Ridgway -->
  <xsl:output method="xml" indent="yes" />
  
  <xsl:template match="/">
    <xsl:apply-templates select="/sourcemonitor_metrics" />
  </xsl:template>
  
<xsl:variable name="newline">
<xsl:text>
</xsl:text>
</xsl:variable>

<xsl:variable name="spaces">
<xsl:text>  </xsl:text>
</xsl:variable>


  <!-- Transform the results into a simpler more intuitive and summarised format -->
  <xsl:template match="sourcemonitor_metrics">
      <xsl:copy-of select="$newline" />
      <SourceMonitorComplexitySummary>
      <xsl:copy-of select="$newline" />
        <MostComplexMethods>
      <xsl:copy-of select="$newline" />
          <xsl:call-template name="MostComplexMethods"/>
        </MostComplexMethods>
      <xsl:copy-of select="$newline" />
      <xsl:copy-of select="$newline" />
        <MostDeeplyNestedCode>
      <xsl:copy-of select="$newline" />
          <xsl:call-template name="MostDeeplyNestedCode"/>
        </MostDeeplyNestedCode>
      <xsl:copy-of select="$newline" />
      <xsl:copy-of select="$newline" />
        <MostLines>
      <xsl:copy-of select="$newline" />
          <xsl:call-template name="MostLines"/>
        </MostLines>
      <xsl:copy-of select="$newline" />
      <xsl:copy-of select="$newline" />
        <MostMethods>
      <xsl:copy-of select="$newline" />
          <xsl:call-template name="MostMethods"/>
        </MostMethods>
      <xsl:copy-of select="$newline" />
      </SourceMonitorComplexitySummary>
      <xsl:copy-of select="$newline" />
  </xsl:template>

  <!-- Complexity Metrics -->
  <xsl:template name="MostComplexMethods">
    <xsl:for-each select=".//file">
      <xsl:sort select="metrics/metric[@id='M10']" order="descending" data-type="number" />
      
      <xsl:choose>
        <xsl:when test="position() &lt; 16">
           <xsl:copy-of select="$spaces" /><Method>
      <xsl:copy-of select="$newline" />
              <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><File><xsl:value-of select="@file_name"/></File>
      <xsl:copy-of select="$newline" />
              <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Name><xsl:value-of select="metrics/metric[@id='M9']"/></Name>
      <xsl:copy-of select="$newline" />
              <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Line><xsl:value-of select="metrics/metric[@id='M8']"/></Line>
      <xsl:copy-of select="$newline" />
              <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Complexity><xsl:value-of select="metrics/metric[@id='M10']"/></Complexity>
      <xsl:copy-of select="$newline" />
           <xsl:copy-of select="$spaces" /></Method>
      <xsl:copy-of select="$newline" />
        </xsl:when>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <!-- Nesting Metrics -->
  <xsl:template name="MostDeeplyNestedCode">
    <xsl:for-each select=".//file">
        <xsl:sort select="metrics/metric[@id='M12']" order="descending" data-type="number" />
      
        <xsl:choose>
          <xsl:when test="position() &lt; 16">
             <xsl:copy-of select="$spaces" /><Block>
      <xsl:copy-of select="$newline" />
                <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><File><xsl:value-of select="@file_name"/></File>
      <xsl:copy-of select="$newline" />
                <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Depth><xsl:value-of select="metrics/metric[@id='M12']"/></Depth>
      <xsl:copy-of select="$newline" />
                <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Line><xsl:value-of select="metrics/metric[@id='M11']"/></Line>
      <xsl:copy-of select="$newline" />
             <xsl:copy-of select="$spaces" /></Block>
      <xsl:copy-of select="$newline" />
          </xsl:when>
        </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <!-- Line count Metrics -->
  <xsl:template name="MostLines">
    <xsl:for-each select=".//file">
      <xsl:sort select="metrics/metric[@id='M0']" order="descending" data-type="number" />

      <xsl:choose>
        <xsl:when test="position() &lt; 16">
          <xsl:copy-of select="$spaces" /><File>
      <xsl:copy-of select="$newline" />
            <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Name><xsl:value-of select="@file_name"/></Name>
      <xsl:copy-of select="$newline" />
            <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Lines><xsl:value-of select="metrics/metric[@id='M0']"/></Lines>
      <xsl:copy-of select="$newline" />
          <xsl:copy-of select="$spaces" /></File>
      <xsl:copy-of select="$newline" />
        </xsl:when>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <!-- Method count Metrics -->
  <xsl:template name="MostMethods">
    <xsl:for-each select=".//file">
      <xsl:sort select="metrics/metric[@id='M5']" order="descending" data-type="number" />

      <xsl:choose>
        <xsl:when test="position() &lt; 16">
          <xsl:copy-of select="$spaces" /><File>
      <xsl:copy-of select="$newline" />
            <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Name><xsl:value-of select="@file_name"/></Name>
      <xsl:copy-of select="$newline" />
            <xsl:copy-of select="$spaces" /><xsl:copy-of select="$spaces" /><Methods><xsl:value-of select="metrics/metric[@id='M5']"/></Methods>
      <xsl:copy-of select="$newline" />
          <xsl:copy-of select="$spaces" /></File>
      <xsl:copy-of select="$newline" />
        </xsl:when>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>
