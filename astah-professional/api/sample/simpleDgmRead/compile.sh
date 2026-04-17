#!/bin/sh
# Shell file to compile a sample of Astah API on Windows

# Remove # from following two lines, if you'd like to use jdk.
# JAVA_HOME=/usr/lib/java7
# PATH=$JAVA_HOME/bin:$PATH

ASTAH_HOME=../../../
# ASTAH_HOME="/Applications/astah professional/astah professional.app/Contents/Java"
#ASTAH_COM_JAR="$ASTAH_HOME/astah-community.jar"
#ASTAH_UML_JAR="$ASTAH_HOME/astah-uml.jar"
#ASTAH_PRO_JAR="$ASTAH_HOME/astah-pro.jar"
#ASTAH_JAR="$ASTAH_COM_JAR:$ASTAH_UML_JAR:$ASTAH_PRO_JAR"
API_JAR="$ASTAH_HOME/astah-api.jar"
CLASSPATH="$ASTAH_JAR:$API_JAR"

javac -encoding sjis -classpath "$CLASSPATH" *.java